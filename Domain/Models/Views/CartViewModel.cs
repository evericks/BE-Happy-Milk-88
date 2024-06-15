namespace Domain.Models.Views
{
    public class CartViewModel
    {
        public Guid Id { get; set; }

        public ICollection<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
    }
}
