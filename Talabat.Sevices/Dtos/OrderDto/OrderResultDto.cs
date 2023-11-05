using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Sevices.Dtos.OrderDto
{
    public class OrderResultDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public AddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public string Total { get; set; }
    }
}
