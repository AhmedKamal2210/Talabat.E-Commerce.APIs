using System.ComponentModel.DataAnnotations;

namespace Talabat.Sevices.Dtos.BasketDto
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemsDto> BasketItems { get; set; } 
        public int? DeliveryMethodId { get; set; }
        public decimal ShappingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
