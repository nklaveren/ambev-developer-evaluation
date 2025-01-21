using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleTests
{
    private readonly ISaleRepository _saleRepositoryMock = Substitute.For<ISaleRepository>();
    private UpdateSaleHandler _handler;

    public UpdateSaleTests()
    {
        _handler = new UpdateSaleHandler(_saleRepositoryMock);
    }

    [Fact(DisplayName = "Should update sale when valid data is provided")]
    public async Task ShouldUpdateSale_WhenValidDataIsProvided()
    {
        var sale = SaleTestData.GetValidSaleWithItems();
        _saleRepositoryMock.GetByIdAsync(sale.Id).Returns(Task.FromResult(sale));
        var command = new UpdateSaleCommand(sale.Id, sale.CustomerId, sale.BranchId, [.. sale.Items.Select(item => new UpdateSaleItemCommand(item.Id, item.Quantity, item.UnitPrice.Amount))]);
        var result = await _handler.Handle(command, CancellationToken.None);
        await _saleRepositoryMock.Received(1).UpdateAsync(Arg.Any<Sale>());
        Assert.NotNull(result);
    }
}
