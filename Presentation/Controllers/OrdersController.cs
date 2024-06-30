using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrders([FromRoute] Guid id)
        {
            try
            {
                return await _orderService.GetOrder(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetOrders([FromBody] OrderFilterModel filter)
        {
            try
            {
                return await _orderService.GetOrders(filter);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateModel model)
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _orderService.CreateOrder(auth.Id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }

        }
    }
}
