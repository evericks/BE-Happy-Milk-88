namespace Domain.Models.Views
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; } = null!;
    }
}
