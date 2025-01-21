using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class SaleServiceTests
{
    private readonly ISaleRepository _saleRepositoryMock = Substitute.For<ISaleRepository>();
    private readonly ISaleService _saleService;

    public SaleServiceTests()
    {
        _saleService = new SaleService(_saleRepositoryMock);
    }

    [Fact(DisplayName = "Should create sale when valid data is provided")]
    public async Task ShouldCreateSale_WhenValidDataIsProvided()
    {
        var saleItems = SaleItemTestData.GetSaleItems(10, Guid.Empty);
        var sale = await _saleService.CreateAsync(Guid.NewGuid(), Guid.NewGuid(), saleItems);
        await _saleRepositoryMock.Received(1).AddAsync(sale);
        Assert.NotNull(sale);
    }

    [Fact(DisplayName = "Should cancel sale when valid data is provided")]
    public async Task ShouldCancelSale_WhenValidDataIsProvided()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(sale);
        var saleCanceled = await _saleService.CancelAsync(sale.Id);
        await _saleRepositoryMock.Received(1).UpdateAsync(saleCanceled);
        Assert.NotNull(saleCanceled);
    }

    [Fact(DisplayName = "Should cancel item when valid data is provided")]
    public async Task ShouldCancelItem_WhenValidDataIsProvided()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(sale);
        var saleItem = sale.Items.First();
        var saleItemCanceled = await _saleService.CancelItemAsync(sale.Id, saleItem.ProductId);
        await _saleRepositoryMock.Received(1).UpdateAsync(saleItemCanceled);
        Assert.NotNull(saleItemCanceled);
    }

    [Fact(DisplayName = "Should update sale when valid data is provided")]
    public async Task ShouldUpdateSale_WhenValidDataIsProvided()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(sale);
        var saleUpdated = await _saleService.UpdateAsync(sale);
        await _saleRepositoryMock.Received(1).UpdateAsync(saleUpdated);
        Assert.NotNull(saleUpdated);
    }
}
