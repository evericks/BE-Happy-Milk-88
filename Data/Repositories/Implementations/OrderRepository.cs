using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(HappyMilkContext context) : base(context)
        {
        }
    }
}
