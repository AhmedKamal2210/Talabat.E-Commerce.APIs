
using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Repository.IRepository;

namespace Talabat.Repository.Repsository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketByIdAsync(string basketId)
            => await _database.KeyDeleteAsync(basketId); 

        public async Task<CustomerBasket> GetBasketByIdAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(3));

            if (!isCreated)
                return null;

            return await GetBasketByIdAsync(basket.Id);
        }
    }
}
