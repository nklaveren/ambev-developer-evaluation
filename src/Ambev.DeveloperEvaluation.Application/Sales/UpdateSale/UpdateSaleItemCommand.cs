namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemCommand
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public UpdateSaleItemCommand(Guid id, int quantity, decimal price)
    {
        Id = id;
        Quantity = quantity;
        Price = price;
    }
}
