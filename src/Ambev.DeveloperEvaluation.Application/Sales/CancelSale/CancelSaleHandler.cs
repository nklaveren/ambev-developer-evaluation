using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleService _saleService;
    public CancelSaleHandler(ISaleService saleService)
    {
        _saleService = saleService;
    }
    public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleService.CancelAsync(command.Id);
        return new CancelSaleResult { Id = sale.Id };
    }
}
