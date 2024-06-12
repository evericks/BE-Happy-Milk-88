using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(HappyMilkContext context) : base(context)
        {
        }
    }
}
