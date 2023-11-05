using System.ComponentModel.DataAnnotations;

namespace Talabat.Sevices.Dtos.BasketDto
{
    public class BasketItemsDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1 , double.MaxValue , ErrorMessage = "Price Must be Greater than Zero !") ]
        public decimal Price { get; set; }
        [Required]
        [Range(1 , 10 , ErrorMessage = "Quntity Must be Between 1 and 10 Pieces !")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
    }
}