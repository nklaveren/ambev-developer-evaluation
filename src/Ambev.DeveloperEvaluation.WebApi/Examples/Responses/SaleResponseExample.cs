using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;
using Swashbuckle.AspNetCore.Filters;

namespace Ambev.DeveloperEvaluation.WebApi.Examples.Responses;

public class SaleResponseExample : IExamplesProvider<GetAllSaleResponse>
{
    public GetAllSaleResponse GetExamples()
    {
        var sale = new GetAllSaleResponse
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            TotalAmount = 100.00m,
            Items = [
                new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    Quantity = 5,
                    UnitPrice = 10.00m
                }
            ]
        };
        return sale;
    }
}

public class SaleListResponseExample : IExamplesProvider<IEnumerable<GetAllSaleResponse>>
{
    public IEnumerable<GetAllSaleResponse> GetExamples()
    {
        var sales = new List<GetAllSaleResponse>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    BranchId = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    TotalAmount = 100.00m,
                    Items = [
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            Quantity = 5,
                            UnitPrice = 10.00m
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            Quantity = 15,
                            UnitPrice = 20.00m
                        }
                    ]
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    BranchId = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    TotalAmount = 100.00m,
                    Items = [
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            Quantity = 5,
                            UnitPrice = 10.00m
                        }
                    ]
                }
            };

        return sales;
    }
}