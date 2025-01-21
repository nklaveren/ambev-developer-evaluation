using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CancelSaleItemTests
{
    private readonly ISaleRepository _saleRepositoryMock = Substitute.For<ISaleRepository>();
    private readonly CancelSaleItemHandler _handler;

    public CancelSaleItemTests()
    {
        _handler = new CancelSaleItemHandler(_saleRepositoryMock);
    }

    [Fact(DisplayName = "Should cancel sale item when valid data is provided")]
    public async Task ShouldCancelSaleItem_WhenValidDataIsProvided()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(Task.FromResult(sale));

        var command = new CancelSaleItemCommand(sale.Id, sale.Items.First().Id);
        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should throw exception when sale item is not found")]
    public async Task ShouldThrowException_WhenSaleItemIsNotFound()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(Task.FromResult(sale));
        var command = new CancelSaleItemCommand(sale.Id, Guid.NewGuid());
        await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact(DisplayName = "Should throw exception when sale is not found")]
    public async Task ShouldThrowException_WhenSaleIsNotFound()
    {
        var command = new CancelSaleItemCommand(Guid.NewGuid(), Guid.NewGuid());
        await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
