using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;

/// <summary>
/// The response for the Get Sale Item endpoints
/// </summary>
public class GetAllSaleItemResponse
{
    /// <summary>
    /// The ID of the sale item
    /// </summary>
    [SwaggerSchema(Description = "The ID of the sale item")]
    public Guid Id { get; set; }
    /// <summary>
    /// The ID of the product
    /// </summary>
    [SwaggerSchema(Description = "The ID of the product")]
    public Guid ProductId { get; set; }
    /// <summary>
    /// The quantity of the product
    /// </summary>
    [SwaggerSchema(Description = "The quantity of the product")]
    public int Quantity { get; set; }
    /// <summary>
    /// The unit price of the product
    /// </summary>
    [SwaggerSchema(Description = "The unit price of the product")]
    public decimal UnitPrice { get; set; }
}
