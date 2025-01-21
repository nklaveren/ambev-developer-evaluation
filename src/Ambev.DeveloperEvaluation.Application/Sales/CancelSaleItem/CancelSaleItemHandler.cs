using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
{
    private readonly ISaleRepository _saleRepository;
    public CancelSaleItemHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }
    public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id) ??
          throw new DomainException($"Sale with ID {command.Id} not found");

        var item = sale.Items.FirstOrDefault(i => i.Id == command.ItemId) ??
            throw new DomainException($"Item with Product ID {command.ItemId} not found in sale {command.Id}");

        item.Cancel();
        await _saleRepository.UpdateAsync(sale);

        return new CancelSaleItemResult { Id = sale.Id, ItemId = command.ItemId };
    }
}
