using AutoMapper;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.Entities.OrderEntities;
using Talabat.Core.Entities.ProductEntities;
using Talabat.Repository.IRepository;
using Talabat.Repository.Specifications;
using Talabat.Sevices.Dtos.OrderDto;
using Talabat.Sevices.IServices.IBasketServices;
using Talabat.Sevices.IServices.IOrderServices;
using Talabat.Sevices.IServices.IPaymentServices;

namespace Talabat.Sevices.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketServices _basketServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentServices _paymentServices;

        public OrderServices(
                    IBasketServices basketServices,
                    IUnitOfWork unitOfWork,
                    IMapper mapper,
                    IPaymentServices paymentServices)
        {
            _basketServices = basketServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentServices = paymentServices;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto)
        {
            // Get basket
            var basket = await _basketServices.GetBasketByIdAsync(orderDto.BasketId);

            if (basket is null)
                return null;

            // Fill Order Items From Basket Items
            var orderItems = new List<OrderItemDto>();
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItems(productItem.Price, item.Quantity, itemOrdered);

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedOrderItem);
            }

            // Get Delivery Method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethods>().GetByIdAsync(orderDto.DeliveryMethodId);

            // Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity); // the sub total price without shipping price!

            //ToDo => Check if Order Exist. (Payment)
            var specs = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Orders>().GetWithSpecsAsync(specs);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Orders>().Delete(existingOrder);
                await _paymentServices.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }

            // Create order
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
            var mappedOrderItems = _mapper.Map<List<OrderItems>>(orderItems);

            var order = new Orders(orderDto.BuerEmail, mappedShippingAddress, deliveryMethod, mappedOrderItems, subTotal, basket.PaymentIntentId);

            await _unitOfWork.Repository<Orders>().Add(order);
            await _unitOfWork.Complete();

            // Delete Basket
            await _basketServices.DeleteBasketByIdAsync(orderDto.BasketId);

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;

        }

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsnc(string buyerEmail)
        {
            var specs = new OrderWithItemSpecifications(buyerEmail); 

            var orders = await _unitOfWork.Repository<Orders>().GetAllWithSpecsAsync(specs);
            var mappedOrders = _mapper.Map<IReadOnlyList<OrderResultDto>>(orders);

            return mappedOrders;
        }

        public async Task<IReadOnlyList<DeliveryMethods>> GetDeliveryMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethods>().GetAllAsync();

        public async Task<OrderResultDto> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var specs = new OrderWithItemSpecifications(id, buyerEmail);

            var order = await _unitOfWork.Repository<Orders>().GetWithSpecsAsync(specs);
            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }
    }
}
