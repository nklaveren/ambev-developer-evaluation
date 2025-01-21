using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public CreateSaleCommand(Guid customerId, Guid branchId, List<CreateSaleItemCommand> items)
    {
        CustomerId = customerId;
        BranchId = branchId;
        Items = items;
    }

    public Guid BranchId { get; set; }
    public Guid CustomerId { get; set; }
    public List<CreateSaleItemCommand> Items { get; set; } = [];
}
