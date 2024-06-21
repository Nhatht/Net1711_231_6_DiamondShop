using System;
using System.Collections.Generic;

namespace DiamondShopData.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int? SaleStaffId { get; set; }

    public int? DeliveryStaffId { get; set; }

    public int PaymentId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public long TotalPrice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Customer? DeliveryStaff { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual Customer? SaleStaff { get; set; }
}
