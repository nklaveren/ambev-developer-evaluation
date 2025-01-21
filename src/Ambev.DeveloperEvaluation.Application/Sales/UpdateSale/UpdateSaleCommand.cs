using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }

    public UpdateSaleCommand(Guid id, Guid customerId, Guid branchId, List<UpdateSaleItemCommand> items)
    {
        Id = id;
        CustomerId = customerId;
        BranchId = branchId;
        Items = items;
    }

    public List<UpdateSaleItemCommand> Items { get; set; } = [];
}
