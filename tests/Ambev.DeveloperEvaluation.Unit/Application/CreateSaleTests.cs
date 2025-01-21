using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleTests
{
    private readonly ISaleRepository _saleRepositoryMock = Substitute.For<ISaleRepository>();
    private readonly CreateSaleHandler _handler;

    public CreateSaleTests()
    {
        _handler = new CreateSaleHandler(_saleRepositoryMock);
    }

    [Fact(DisplayName = "Should create sale when valid data is provided")]
    public async Task ShouldCreateSale_WhenValidDataIsProvided()
    {
        var sale = await _handler.Handle(new CreateSaleCommand(Guid.NewGuid(), Guid.NewGuid(), [CreateSaleItemCommandTestData.CreateItemValid()]), CancellationToken.None);
        await _saleRepositoryMock.Received(1).AddAsync(Arg.Any<Sale>());
        Assert.NotNull(sale);
    }
}
