using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
{
    private readonly ISaleService _saleService;
    public CancelSaleItemHandler(ISaleService saleService)
    {
        _saleService = saleService;
    }
    public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleService.CancelItemAsync(command.Id, command.ItemId);
        return new CancelSaleItemResult { Id = sale.Id, ItemId = command.ItemId };
    }
}
