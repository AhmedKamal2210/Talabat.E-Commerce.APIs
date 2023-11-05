using Talabat.Core.Entities;
using Talabat.Sevices.Dtos.BasketDto;

namespace Talabat.Sevices.IServices.IBasketServices
{
    public interface IBasketServices
    {
        Task<CustomerBasketDto> GetBasketByIdAsync(string basketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketByIdAsync(string basketId);
    }
}
