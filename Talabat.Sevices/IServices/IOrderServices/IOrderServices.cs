using Talabat.Core.Entities.BasketEntities;
using Talabat.Sevices.Dtos.OrderDto;

namespace Talabat.Sevices.IServices.IOrderServices
{
    public interface IOrderServices
    {
        Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsnc(string buyerEmail);
        Task<OrderResultDto> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethods>> GetDeliveryMethodsAsync();
    }
}
