using Domain.Models.Pagination;

namespace Domain.Models.Filters
{
    public class OrderFilterModel
    {
        public PaginationRequestModel Pagination { get; set; } = new PaginationRequestModel();
    }
}
