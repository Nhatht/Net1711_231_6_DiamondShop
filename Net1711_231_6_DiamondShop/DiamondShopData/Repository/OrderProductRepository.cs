using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondShopData.Base;
using Microsoft.EntityFrameworkCore;
using DiamondShopData.Models;

namespace OrderData.Repository
{
    public class OrderProductRepository : GenericRepository<OrderProduct>
    {
        private readonly Net17112316DiamondShopContext _context;
        public OrderProductRepository(Net17112316DiamondShopContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<List<OrderProduct>> GetByOrderId(int orderId) => await _context.OrderProducts
            .Where(x => x.OrderId == orderId).ToListAsync();
    }
}
