using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
    }

    public async Task<Sale> CreateAsync(Guid customerId, Guid branchId, List<SaleItem> items)
    {
        //TODO: Implement customer validation
        // var customer = await _customerRepository.GetByIdAsync(customerId) ??
        //     throw new DomainException($"Customer with ID {customerId} not found");

        //TODO: Implement branch validation
        // var branch = await _branchRepository.GetByIdAsync(branchId) ??
        //     throw new DomainException($"Branch with ID {branchId} not found");

        var sale = new Sale(Guid.NewGuid(), customerId, branchId, items);
        foreach (var item in sale.Items)
        {
            await ValidateSaleItemQuantityAsync(item);
        }
        await _saleRepository.AddAsync(sale);
        return sale;
    }

    public async Task<Sale> CancelAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId) ?? throw new DomainException($"Sale with ID {saleId} not found");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale);

        return sale;
    }

    public async Task<Sale> CancelItemAsync(Guid saleId, Guid productId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId) ??
            throw new DomainException($"Sale with ID {saleId} not found");

        var item = sale.Items.FirstOrDefault(i => i.ProductId == productId) ??
            throw new DomainException($"Item with Product ID {productId} not found in sale {saleId}");
        item.Cancel();
        await _saleRepository.UpdateAsync(sale);

        return sale;
    }

    public async Task<Sale> UpdateAsync(Sale alteredSale)
    {
        var sale = await _saleRepository.GetByIdAsync(alteredSale.Id) ?? throw new DomainException($"Sale with ID {alteredSale.Id} not found");
        sale.Update(alteredSale.CustomerId, alteredSale.BranchId, [.. alteredSale.Items]);
        await _saleRepository.UpdateAsync(sale);
        return sale;
    }

    private static Task<bool> ValidateSaleItemQuantityAsync(SaleItem saleItem)
    {
        var maxQuantitySpec = new MaxItemQuantitySpecification();

        if (!maxQuantitySpec.IsSatisfiedBy(saleItem))
            throw new DomainException($"Maximum quantity per item is {MaxItemQuantitySpecification.GetMaxQuantity()}");

        return Task.FromResult(true);
    }
}