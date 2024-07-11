using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondShopData.Base;
using DiamondShopData.Models;
using DiamondShopData.ViewModel.OrderDTO;
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

        public async Task<List<OrderDTO>> GetOrder()
        {
            var order = await _context.Orders.Include(x => x.Customer).Include(x => x.Payment).ToListAsync();
            List<OrderDTO> list = new List<OrderDTO>();
            foreach (var o in order)
            {
                var p = new OrderDTO
                {
                    Id = o.Id,
                    CustomerName = o.Customer.Name,
                    Payment = o.Payment.Name,
                    CreatedDate = o.CreatedDate,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice
                };
                list.Add(p);
            }

            return list;
        }

        public async Task<OrderDTO> GetOrderDtO(int id)
        {
            var order = await _context.Orders.Include(x => x.Customer).Include(x => x.Payment).FirstOrDefaultAsync(x => x.Id == id);
            var p = new OrderDTO
            {
                Id = order.Id,
                CustomerName = order.Customer.Name,
                Payment = order.Payment.Name,
                CreatedDate = order.CreatedDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice
            };
            return p;
        }

    }
}
