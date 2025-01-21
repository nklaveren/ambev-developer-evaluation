using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public Guid BranchId { get; set; }
    public Guid CustomerId { get; set; }
    public List<CreateSaleItemCommand> Items { get; set; } = [];
}
