namespace Talabat.Sevices.Dtos.OrderDto
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string BuerEmail { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
