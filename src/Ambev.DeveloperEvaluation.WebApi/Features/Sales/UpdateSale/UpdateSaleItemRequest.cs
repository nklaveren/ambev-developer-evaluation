
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleItemRequest
{
    public string Id { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
