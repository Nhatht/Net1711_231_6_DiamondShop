using System;
using System.Collections.Generic;

namespace DiamondShopData.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
