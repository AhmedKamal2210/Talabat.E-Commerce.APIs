using AutoMapper;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Repository.IRepository;
using Talabat.Sevices.Dtos.BasketDto;
using Talabat.Sevices.IServices.IBasketServices;

namespace Talabat.Sevices.Services.BasketSevices
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketByIdAsync(string basketId)
            => await _basketRepository.DeleteBasketByIdAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketByIdAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketByIdAsync(basketId);

            if (basket is null)
                return new CustomerBasketDto();

            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);

            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        { 
            var customerBasket = _mapper.Map<CustomerBasket>(basket);

            var updatedasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            var mappedCustomerBasket = _mapper.Map<CustomerBasketDto>(updatedasket);

            return mappedCustomerBasket;
        }
    }
}
