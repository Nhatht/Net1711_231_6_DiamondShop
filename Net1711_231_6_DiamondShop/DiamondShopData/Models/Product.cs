using System;
using System.Collections.Generic;

namespace DiamondShopData.Models;

public partial class Product
{
    public int Id { get; set; }

    public int DiamondId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Metal { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal Cost { get; set; }

    public long Size { get; set; }

    public int Stock { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Diamond Diamond { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
