using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(HappyMilkContext context) : base(context)
        {
        }
    }
}
