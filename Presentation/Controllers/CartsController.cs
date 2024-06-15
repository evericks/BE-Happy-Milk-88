using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Creates;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize(UserRoles.CUSTOMER)]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _cartService.GetCart(auth.Id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.CUSTOMER)]
        public async Task<IActionResult> AddToCart([FromBody] CartItemCreateModel model)
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _cartService.AddToCart(auth.Id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.CUSTOMER)]
        [Route("items/{id}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] Guid id, [FromBody] CartItemUpdateModel model)
        {
            try
            {
                return await _cartService.UpdateCartItem(model.Id, model.Quantity);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpDelete]
        [Authorize(UserRoles.CUSTOMER)]
        [Route("items/{id}")]
        public async Task<IActionResult> RemoveCartItem([FromRoute] Guid id)
        {
            try
            {
                return await _cartService.RemoveCartItem(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
