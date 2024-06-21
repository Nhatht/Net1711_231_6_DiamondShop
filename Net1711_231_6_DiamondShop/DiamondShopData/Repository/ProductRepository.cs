using DiamondShopData.ViewModel.ProductDTO;
using DiamondShopData.Base;
using DiamondShopData.DAO;
using DiamondShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(Net17112316DiamondShopContext context) : base(context)
        {
        }
    }
}
