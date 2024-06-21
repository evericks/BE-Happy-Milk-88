﻿namespace Domain.Models.Creates
{
    public class OrderCreateModel
    {
        public int Amount { get; set; }

        public string Receiver { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string PaymentMethod { get; set; } = null!;

        public ICollection<OrderDetailCreateModel> OrderDetails { get; set; } = new List<OrderDetailCreateModel>();
    }
}
