using System;
using System.Collections.Generic;

namespace DiamondShopData.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? IsDeleted { get; set; }
}
