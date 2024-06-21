using System;
using System.Collections.Generic;

namespace DiamondShopData.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Order> OrderCustomers { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderDeliveryStaffs { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderSaleStaffs { get; set; } = new List<Order>();
}
