using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemRequest>
{
    public CancelSaleItemValidator()
    {
        RuleFor(x => x.SaleItemId).NotEmpty();
        RuleFor(x => x.SaleId).NotEmpty();
    }
}
