using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Entities.BasketEntities
{
    public class DeliveryMethods : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // remove identity for id.
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}
