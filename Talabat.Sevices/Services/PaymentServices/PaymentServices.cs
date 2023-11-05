using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.Entities.OrderEntities;
using Talabat.Repository.IRepository;
using Talabat.Repository.Specifications;
using Talabat.Sevices.Dtos.BasketDto;
using Talabat.Sevices.Dtos.OrderDto;
using Talabat.Sevices.IServices.IBasketServices;
using Talabat.Sevices.IServices.IPaymentServices;
using Product = Talabat.Core.Entities.ProductEntities.Product;

namespace Talabat.Sevices.Services.PaymentServices
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basketServices;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PaymentServices(
            IUnitOfWork unitOfWork,
            IBasketServices basketServices,
            IConfiguration configuration,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketServices = basketServices;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            var basket = await _basketServices.GetBasketByIdAsync(basketId);

            if (basket == null)
                return null;

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethods>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                    item.Price = productItem.Price;
            }

            var services = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "Card" }
                };

                intent = await services.CreateAsync(option);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + ((long)shippingPrice * 100),
                };
                await services.UpdateAsync(basket.PaymentIntentId, option);
            }

            await _basketServices.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderWithItemSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Orders>().GetWithSpecsAsync(spec);

            if (order == null)
                return null;

            order.OrderStatus = OrderStatus.PaymentFailed;

            _unitOfWork.Repository<Orders>().Update(order);
            await _unitOfWork.Complete();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceded(string paymentIntentId)
        {
            var spec = new OrderWithItemSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Orders>().GetWithSpecsAsync(spec);

            if (order == null)
                return null;

            order.OrderStatus = OrderStatus.PaymentReceived;

            _unitOfWork.Repository<Orders>().Update(order);
            await _unitOfWork.Complete();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }
    }
}
