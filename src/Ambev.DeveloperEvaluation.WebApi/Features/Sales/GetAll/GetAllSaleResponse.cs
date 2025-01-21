using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;
/// <summary>
/// The response for the Get Sale endpoints
/// </summary>
public class GetAllSaleResponse
{
    /// <summary>
    /// The ID of the sale
    /// </summary>
    [SwaggerSchema(Description = "The ID of the sale")]
    public Guid Id { get; set; }
    /// <summary>
    /// The number of the sale
    /// </summary>
    [SwaggerSchema(Description = "The number of the sale")]
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the customer
    /// </summary>
    [SwaggerSchema(Description = "The ID of the customer")]
    public Guid CustomerId { get; set; }
    /// <summary>
    /// The ID of the branch
    /// </summary>
    [SwaggerSchema(Description = "The ID of the branch")]
    public Guid BranchId { get; set; }
    /// <summary>
    /// The date and time the sale was created
    /// </summary>
    [SwaggerSchema(Description = "The date and time the sale was created")]
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// The date and time the sale was updated
    /// </summary>
    [SwaggerSchema(Description = "The date and time the sale was updated")]
    public DateTime UpdatedAt { get; set; }
    /// <summary>
    /// The total amount of the sale
    /// </summary>
    [SwaggerSchema(Description = "The total amount of the sale")]
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// The items of the sale
    /// </summary>
    [SwaggerSchema(Description = "The items of the sale")]
    public List<GetAllSaleItemResponse> Items { get; set; } = [];
}
