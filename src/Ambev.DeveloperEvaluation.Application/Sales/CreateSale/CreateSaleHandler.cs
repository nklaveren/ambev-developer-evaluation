using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;

    public CreateSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var items = command.Items.Select(item => new SaleItem(Guid.NewGuid(), Guid.Empty, item.ProductId, item.Quantity, item.UnitPrice)).ToList();
        var sale = new Sale(Guid.NewGuid(), command.CustomerId, command.BranchId, items);
        await _saleRepository.AddAsync(sale);
        return new CreateSaleResult { SaleId = sale.Id };
    }
}
