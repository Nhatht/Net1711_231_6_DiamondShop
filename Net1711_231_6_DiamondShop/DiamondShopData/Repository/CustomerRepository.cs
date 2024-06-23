using DiamondShopData.Base;
using DiamondShopData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(Net17112316DiamondShopContext context) : base(context)
        {
        }
    }
}
