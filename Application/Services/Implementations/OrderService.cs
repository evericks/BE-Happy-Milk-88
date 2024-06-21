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
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _orderRepository = unitOfWork.Order;
            _productRepository  = unitOfWork.Product;
            _cartRepository = unitOfWork.Cart;
        }

        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                var order = await _orderRepository.Where(x => x.Id == id)
                    .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return order != null ? order.Ok() : AppErrors.RECORD_NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateOrder(Guid customerId, OrderCreateModel model)
        {
            try
            {
                if (model.PaymentMethod.Equals(PaymentMethods.VNPAY))
                {
                    return AppErrors.INVALID_PAYMENT_METHOD.BadRequest();
                }
                if (model.PaymentMethod.Equals(PaymentMethods.CASH))
                {
                    var order = _mapper.Map<Order>(model);
                    order.CustomerId = customerId;
                    order.IsPayment =false;
                    order.Status = OrderStatuses.PENDING;
                    _orderRepository.Add(order);
                    var result = await _unitOfWork.SaveChangesAsync();
                    if (result > 0)
                    {
                        await CalculateProductQuantity(order);
                        await ClearCart(customerId);
                        return await GetOrder(order.Id);
                    }
                }
                return AppErrors.CREATE_FAIL.UnprocessableEntity();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task ClearCart(Guid customerId)
        {
            try
            {
                var cart = await _cartRepository.Where(x => x.CustomerId.Equals(customerId))
                    .Include(x => x.CartItems)
                    .AsTracking()
                    .FirstOrDefaultAsync();
                if (cart == null) {
                    return;
                }
                cart.CartItems = new List<CartItem>();
                _cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task CalculateProductQuantity(Order order)
        {
            try
            {
                var products = new List<Product>();
                foreach (var item in order.OrderDetails)
                {
                    var product = await _productRepository.Where(x => x.Id.Equals(item.ProductId)).FirstOrDefaultAsync();
                    if (product == null)
                    {
                        continue;
                    }
                    product.Quantity =  product.Quantity - item.Quantity;
                    products.Add(product);
                }
                _productRepository.UpdateRange(products);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
