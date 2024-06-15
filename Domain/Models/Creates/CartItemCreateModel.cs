namespace Domain.Models.Creates
{
    public class CartItemCreateModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
