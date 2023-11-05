using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketEntities;

namespace Talabat.Core.Entities.OrderEntities
{
    public enum OrderStatus
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
    public class Orders : BaseEntity
    {
        public Orders()
        {
            
        }

        public Orders(
            string buyerEmail,
            ShippingAddress shippingAddress,
            DeliveryMethods deliveryMethod,
            IReadOnlyList<OrderItems> orderItems,
            decimal subTotal,
            string PaymentIntendId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = PaymentIntendId;
            
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethods DeliveryMethod { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public IReadOnlyList<OrderItems> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }


        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;

    }
}
