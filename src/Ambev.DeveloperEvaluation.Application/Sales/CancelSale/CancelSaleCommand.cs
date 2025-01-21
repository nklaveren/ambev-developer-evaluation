using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

