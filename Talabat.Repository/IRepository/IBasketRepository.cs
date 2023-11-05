using Talabat.Core.Entities.BasketEntities;

namespace Talabat.Repository.IRepository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketByIdAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketByIdAsync(string basketId);

    }
}
