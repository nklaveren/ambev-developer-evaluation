using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    Task<Sale> CreateAsync(Guid customerId, Guid branchId, List<SaleItem> items);
    Task<Sale> CancelAsync(Guid saleId);
    Task<Sale> CancelItemAsync(Guid saleId, Guid productId);
    Task<Sale> UpdateAsync(Sale sale);
}