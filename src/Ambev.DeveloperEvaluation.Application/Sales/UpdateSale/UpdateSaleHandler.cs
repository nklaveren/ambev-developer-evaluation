using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    public UpdateSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id) ?? throw new DomainException($"Sale with ID {command.Id} not found");
        var items = command.Items.Select(item => new SaleItem(Guid.NewGuid(), Guid.Empty, item.Id, item.Quantity, item.Price)).ToList();
        sale.Update(command.CustomerId, command.BranchId, items);
        await _saleRepository.UpdateAsync(sale);
        return new UpdateSaleResult { Id = sale.Id };
    }
}
