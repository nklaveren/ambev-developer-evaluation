using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// The request for the Cancel Sale Item endpoint
/// </summary>
public class CancelSaleItemRequest
{
    /// <summary>
    /// The ID of the sale
    /// </summary>
    [SwaggerSchema(Description = "The ID of the sale")]
    [FromRoute(Name = "id")]
    public Guid SaleId { get; set; }

    /// <summary>
    /// The ID of the sale item
    /// </summary>
    [SwaggerSchema(Description = "The ID of the sale item")]
    [FromRoute(Name = "itemId")]
    public Guid SaleItemId { get; set; }


}
