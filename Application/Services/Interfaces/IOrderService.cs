using Domain.Models.Creates;
using Domain.Models.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IActionResult> GetOrders(OrderFilterModel filter);
        Task<IActionResult> GetCustomerOrders(Guid id, OrderFilterModel filter);
        Task<IActionResult> GetOrder(Guid id);
        Task<IActionResult> CreateOrder(Guid customerId, OrderCreateModel model);
    }
}
