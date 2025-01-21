
using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Functional;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;
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
                    Quantity = 1,
                    UnitPrice = 10.00M
                }
            ]
        };

        var response = await client.PostAsJsonAsync("/api/sales", request);

        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<CreateSaleResponse>();
        Assert.NotNull(responseData);
        Assert.NotEqual(Guid.Empty, responseData.SaleId);
        var dataResponse = await client.GetFromJsonAsync<ApiResponseWithData<GetAllSaleResponse>>($"/api/sales/{responseData.SaleId}");
        var saleResponse = dataResponse?.Data;
        Assert.NotNull(saleResponse);
        Assert.Equal(request.CustomerId, saleResponse.CustomerId);
        Assert.Equal(request.BranchId, saleResponse.BranchId);
        Assert.Equal(request.Items.Count, saleResponse.Items.Count);
        Assert.Equal(request.Items[0].ProductId, saleResponse.Items[0].ProductId);
        Assert.Equal(request.Items[0].Quantity, saleResponse.Items[0].Quantity);
        Assert.Equal(request.Items[0].UnitPrice, saleResponse.Items[0].UnitPrice);
        Assert.Equal(10.00M, saleResponse.TotalAmount);
    }

    [Fact]
    public async Task CreateSale_ShouldReturnBadRequest_WhenCustomerIdIsInvalid()
    {
        var client = _factory.CreateClient();
        var request = new CreateSaleRequest
        {
            CustomerId = Guid.Empty,
            BranchId = Guid.NewGuid(),
            Items = []
        };

        var response = await client.PostAsJsonAsync("/api/sales", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
