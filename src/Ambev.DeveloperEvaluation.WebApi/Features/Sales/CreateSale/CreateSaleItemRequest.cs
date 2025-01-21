using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// The request for the Create Sale Item endpoint
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// The ID of the product being purchased
    /// </summary>
    [SwaggerSchema(Description = "The ID of the product being purchased")]
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product being purchased
    /// </summary>
    [SwaggerSchema(Description = "The quantity of the product being purchased")]
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product being purchased
    /// </summary>
    [SwaggerSchema(Description = "The unit price of the product being purchased")]
    public decimal UnitPrice { get; set; }
}
