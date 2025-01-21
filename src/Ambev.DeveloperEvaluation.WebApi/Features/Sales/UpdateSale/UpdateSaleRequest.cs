
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string BranchId { get; set; } = string.Empty;
    public List<UpdateSaleItemRequest> Items { get; set; } = [];
}
