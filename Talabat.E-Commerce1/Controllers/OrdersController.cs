using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.BasketEntities;
using Talabat.E_Commerce1.HandleRespones;
using Talabat.Sevices.Dtos.OrderDto;
using Talabat.Sevices.IServices.IOrderServices;

namespace Talabat.E_Commerce1.Controllers
{

    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto orderDto)
        {
            var order = await _orderServices.CreateOrderAsync(orderDto);
            if (order is null)
                return BadRequest(new ApiResponse(400, "Error while creating your order!!"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersForUserAsnc()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderServices.GetAllOrdersForUserAsnc(email);

            if (orders.Count() == 0)
                return Ok(new ApiResponse(200, "You Don't Have any Orders yet !!"));

            return Ok(orders);

        }
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderServices.GetOrderByIdAsync(id, buyerEmail);

            if (order is null)
                return  Ok(new ApiResponse(200, $"No Orders With This Id : {id}"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethods>>> GetDeliveryMethodsAsync()
            => Ok(await _orderServices.GetDeliveryMethodsAsync());
    }
}
