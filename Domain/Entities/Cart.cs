﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Cart
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Customer Customer { get; set; } = null!;
}
