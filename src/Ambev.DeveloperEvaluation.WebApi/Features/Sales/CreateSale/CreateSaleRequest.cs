using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// The request for the Create Sale endpoint
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The ID of the customer making the purchase
    /// </summary>
    [SwaggerSchema(Description = "The ID of the customer making the purchase")]
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The ID of the branch where the sale is being made
    /// </summary>
    [SwaggerSchema(Description = "The ID of the branch where the sale is being made")]
    public Guid BranchId { get; set; }

    /// <summary>
    /// The items being purchased
    /// </summary>
    [SwaggerSchema(Description = "The items being purchased")]
    public List<CreateSaleItemRequest>? Items { get; set; }
}
