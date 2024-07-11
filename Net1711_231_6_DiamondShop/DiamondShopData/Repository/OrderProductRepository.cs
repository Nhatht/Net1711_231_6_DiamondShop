using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondShopData.Base;
using Microsoft.EntityFrameworkCore;
using DiamondShopData.Models;
using DiamondShopData.ViewModel.OrderDTO;
using DiamondShopData.ViewModel.ProductDTO;

namespace OrderData.Repository
{
    public class OrderProductRepository : GenericRepository<OrderProduct>
    {
        private readonly Net17112316DiamondShopContext _context;
        public OrderProductRepository(Net17112316DiamondShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderProductDTO>> GetByOrderId(int orderId)
        {
            var order = await _context.OrderProducts.Include(x => x.Product).ThenInclude(x => x.Diamond)
                .Where(x => x.OrderId == orderId).ToListAsync();
            List<OrderProductDTO> list = new List<OrderProductDTO>();
            foreach (var product in order)
            {
                var p = new OrderProductDTO
                {
                    quantity = product.Quantity,
                    Product = new ProductDTO
                    {
                        Id = product.Product.Id,
                        Name = product.Product.Name,
                        Description = product.Product.Description,
                        Metal = product.Product.Metal,
                        ImageUrl = product.Product.ImageUrl,
                        Price = product.Product.Price,
                        Size = product.Product.Size,
                        Cost = product.Product.Cost,
                        DiamondName = product.Product.Diamond.Name
                    }
                };
                list.Add(p);
            }
            return list;
        }
    }
}
