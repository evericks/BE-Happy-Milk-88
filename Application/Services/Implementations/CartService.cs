using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class CartService : BaseService, ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _productRepository = unitOfWork.Product;
            _cartRepository = unitOfWork.Cart;
            _cartItemRepository = unitOfWork.CartItem;
        }

        public async Task<IActionResult> GetCart(Guid customerId)
        {
            try
            {
                var cart = await _cartRepository.Where(x => x.CustomerId.Equals(customerId))
                    .ProjectTo<CartViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                if (cart == null)
                {
                    return AppErrors.CART_NOT_EXIST.NotFound();
                }
                return cart.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> AddToCart(Guid customerId, CartItemCreateModel model)
        {
            try
            {
                var cart = await _cartRepository.Where(x => x.CustomerId.Equals(customerId))
                    .Include(x => x.CartItems)
                    .FirstOrDefaultAsync();
                if (cart == null)
                {
                    return AppErrors.CART_NOT_EXIST.NotFound();
                }
                if (cart.CartItems.Any(x => x.ProductId.Equals(model.ProductId)))
                {
                    foreach (var item in cart.CartItems)
                    {
                        if (item.ProductId.Equals(model.ProductId))
                        {
                            var total = item.Quantity + model.Quantity;
                            if (await CheckProductQuantity(model.ProductId, total))
                            {
                                item.Quantity = total;
                            }
                            else
                            {
                                return AppErrors.PRODUCT_QUANTITY_NOT_ENOUGH.BadRequest();
                            }
                        }
                    }
                }
                else
                {
                    if (await CheckProductQuantity(model.ProductId, model.Quantity))
                    {
                        var newItem = new CartItem()
                        {
                            Id = Guid.NewGuid(),
                            CartId = cart.Id,
                            ProductId = model.ProductId,
                            Quantity = model.Quantity,
                        };
                        cart.CartItems.Add(newItem);
                    }
                    else
                    {
                        return AppErrors.PRODUCT_QUANTITY_NOT_ENOUGH.BadRequest();
                    }

                }
                _cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync();
                return await GetCart(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateCartItem(Guid id, int quantity)
        {
            try
            {
                var cartItem = await _cartItemRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (cartItem == null)
                {
                    return AppErrors.CART_ITEM_NOT_EXIST.BadRequest();
                }
                if (await CheckProductQuantity(cartItem.ProductId, quantity))
                {
                    cartItem.Quantity = quantity;
                    _cartItemRepository.Update(cartItem);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    return AppErrors.PRODUCT_QUANTITY_NOT_ENOUGH.BadRequest();
                }
                return cartItem.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> RemoveCartItem(Guid id)
        {
            try
            {
                var cartItem = await _cartItemRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (cartItem == null)
                {
                    return AppErrors.CART_ITEM_NOT_EXIST.BadRequest();
                }
                _cartItemRepository.Remove(cartItem);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? true.Ok() : false.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> CheckProductQuantity(Guid id, int quantity)
        {
            try
            {
                var product = await _productRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                return product.Quantity >= quantity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}