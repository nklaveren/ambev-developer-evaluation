using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
{
    public CancelSaleItemCommand(Guid id, Guid itemId)
    {
        Id = id;
        ItemId = itemId;
    }

    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
}
