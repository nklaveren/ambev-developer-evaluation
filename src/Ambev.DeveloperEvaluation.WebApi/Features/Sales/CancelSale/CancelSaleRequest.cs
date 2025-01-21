using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// The request for the Cancel Sale endpoint
/// </summary>
public class CancelSaleRequest
{
    /// <summary>
    /// The ID of the sale
    /// </summary>
    [SwaggerSchema(Description = "The ID of the sale")]
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}
