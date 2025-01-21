using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

[ExcludeFromCodeCoverage]
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ConvertUsing(src => new Sale(Guid.NewGuid(), src.CustomerId, src.BranchId, CreateSaleItems(src.Items)));
        CreateMap<CreateSaleItemCommand, SaleItem>();
    }

    private List<SaleItem> CreateSaleItems(List<CreateSaleItemCommand> items) =>
        [.. items.Select(item => new SaleItem(Guid.NewGuid(), Guid.Empty, item.ProductId, item.Quantity, item.UnitPrice))];
}
