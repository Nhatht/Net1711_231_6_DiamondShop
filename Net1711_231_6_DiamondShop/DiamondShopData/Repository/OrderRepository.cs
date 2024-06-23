using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondShopData.Base;
using DiamondShopData.Models;
using Microsoft.EntityFrameworkCore;


namespace OrderData.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        private Net17112316DiamondShopContext _context;
        public OrderRepository(Net17112316DiamondShopContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
