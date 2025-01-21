
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales.CreateSale;

public class CreateSaleTests : IClassFixture<CustomWebApiFactory>
{
    private readonly CustomWebApiFactory _factory;

    public CreateSaleTests(CustomWebApiFactory factory) => _factory = factory;

    [Fact]
    public async Task CreateSale_ShouldReturnSuccess()
    {
        var client = _factory.CreateClient();
        var request = new CreateSaleRequest
        {
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items =
            [
                new CreateSaleItemRequest
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 10,
                    UnitPrice = 10.00M
                }
            ]
        };

        var response = await client.PostAsJsonAsync("/api/sales", request);

        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<CreateSaleResponse>();
        Assert.NotNull(responseData);
        Assert.NotEqual(Guid.Empty, responseData.SaleId);


    }
}
