using Microsoft.AspNetCore.Mvc;
using Talabat.Sevices.Dtos.BasketDto;
using Talabat.Sevices.IServices.IBasketServices;

namespace Talabat.E_Commerce1.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById (string id)
            => Ok(await _basketServices.GetBasketByIdAsync(id));



        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
            => Ok(await _basketServices.UpdateBasketAsync(basket));



        [HttpDelete]
        public async Task<ActionResult> DeleteBasketById(string id)
            => Ok(await _basketServices.DeleteBasketByIdAsync(id));
    }
}
