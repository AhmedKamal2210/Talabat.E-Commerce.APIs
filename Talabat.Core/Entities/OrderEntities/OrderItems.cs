namespace Talabat.Core.Entities.OrderEntities
{
    public class OrderItems : BaseEntity
    {
        public OrderItems()
        {
            
        }

        public OrderItems(
            decimal price,
            int quantity,
            ProductItemOrdered itemOrdered)
        {
            Price = price;
            Quantity = quantity;
            ItemOrdered = itemOrdered;
        }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }
    }
}
