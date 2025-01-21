using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Sale> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new KeyNotFoundException($"Sale with id {id} not found");
    }

    public override async Task<Sale> AddAsync(Sale sale)
    {
        foreach (var item in sale.Items)
        {
            item.UpdateSaleId(sale.Id);
        }

        return await base.AddAsync(sale);
    }

    public override async Task<Sale> UpdateAsync(Sale sale)
    {
        var existingSale = await GetByIdAsync(sale.Id);
        if (existingSale != null)
        {
            var itemsToRemove = existingSale.Items
                .Where(existingItem => !sale.Items.Any(newItem => newItem.Id == existingItem.Id))
                .ToList();

            foreach (var item in itemsToRemove)
            {
                Context.Set<SaleItem>().Remove(item);
            }

            foreach (var item in sale.Items)
            {
                if (item.SaleId == Guid.Empty)
                {
                    item.UpdateSaleId(sale.Id);
                    await Context.Set<SaleItem>().AddAsync(item);
                }
                else
                {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }
        }

        return await base.UpdateAsync(sale);
    }
}