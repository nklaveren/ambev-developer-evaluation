using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CancelSaleTests
{
    private readonly ISaleRepository _saleRepositoryMock = Substitute.For<ISaleRepository>();
    private readonly CancelSaleHandler _handler;

    public CancelSaleTests()
    {
        _handler = new CancelSaleHandler(_saleRepositoryMock);
    }

    [Fact(DisplayName = "Should cancel sale when valid data is provided")]
    public async Task ShouldCancelSale_WhenValidDataIsProvided()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(Task.FromResult(sale));
        var command = new CancelSaleCommand(sale.Id);
        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should throw exception when sale is not found")]
    public async Task ShouldThrowException_WhenSaleIsNotFound()
    {
        var command = new CancelSaleCommand(Guid.NewGuid());
        await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
    }
}