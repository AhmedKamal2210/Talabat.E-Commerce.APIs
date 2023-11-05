using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Sevices.Dtos.BasketDto;
using Talabat.Sevices.Dtos.OrderDto;

namespace Talabat.Sevices.IServices.IPaymentServices
{
    public interface IPaymentServices
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId);
        Task<OrderResultDto> UpdateOrderPaymentSucceded(string paymentIntentId);
        Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
