namespace Talabat.Sevices.Dtos.OrderDto
{
    public class OrderItemDto
    {
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureUrl { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
