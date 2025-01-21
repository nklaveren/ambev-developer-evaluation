using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// The response for the Create Sale endpoint
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The ID of the sale
    /// </summary>
    [SwaggerSchema(Description = "The ID of the sale")]
    public Guid SaleId { get; set; }

    /// <summary>
    /// The number of the sale
    /// </summary>
    [SwaggerSchema(Description = "The number of the sale")]
    public string SaleNumber { get; set; } = string.Empty;
}