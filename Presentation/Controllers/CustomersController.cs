using Application.Services.Implementations;
using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomersController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(UserRoles.CUSTOMER)]
        [Route("orders")]
        public async Task<IActionResult> GetCustomerOrders([FromBody] OrderFilterModel filter)
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _orderService.GetCustomerOrders(auth.Id, filter);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateModel model)
        {
            try
            {
                return await _customerService.CreateCustomer(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
