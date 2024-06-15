using Domain.Models.Creates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<IActionResult> GetCart(Guid customerId);
        Task<IActionResult> AddToCart(Guid cartId, CartItemCreateModel model);
        Task<IActionResult> UpdateCartItem(Guid id, int quantity);
        Task<IActionResult> RemoveCartItem(Guid id);
    }
}