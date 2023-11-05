namespace Talabat.Core.Entities.BasketEntities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItems> BasketItems { get; set; } = new List<BasketItems>();
        public int? DeliveryMethodId { get; set; }
        public decimal ShappingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
