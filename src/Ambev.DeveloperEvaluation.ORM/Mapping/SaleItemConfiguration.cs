using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : BaseEntityConfiguration<SaleItem>
{
    public override void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        base.Configure(builder);

        builder.Property(si => si.ProductId)
            .IsRequired();

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(si => si.IsCanceled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(s => s.Id)
         .IsRequired()
         .HasMaxLength(50);

        builder.HasOne<Sale>()
            .WithMany(s => s.Items)
            .HasForeignKey("SaleId")
            .HasPrincipalKey(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(si => si.UnitPrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(si => si.TotalAmount, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("TotalAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("ItemCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(si => si.Discount, discount =>
        {
            discount.Property(d => d.Percentage)
                .HasColumnName("DiscountPercentage")
                .HasColumnType("decimal(5,4)")
                .IsRequired();

            discount.OwnsOne(d => d.Amount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("DiscountAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("DiscountCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            discount.Property(d => d.Reason)
                .HasColumnName("DiscountReason")
                .HasMaxLength(200);
        });

        // Create indexes
        builder.HasIndex(si => si.ProductId);
        builder.HasIndex(si => si.IsCanceled);
    }
}