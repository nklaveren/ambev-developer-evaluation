using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleService _saleService;

    public CreateSaleHandler(ISaleService saleService)
    {
        _saleService = saleService;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var items = command.Items.Select(item => new SaleItem(Guid.NewGuid(), Guid.Empty, item.ProductId, item.Quantity, item.UnitPrice)).ToList();
        var sale = await _saleService.CreateAsync(command.CustomerId, command.BranchId, items);
        return new CreateSaleResult { SaleId = sale.Id };
    }
}
