using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.ViewModel.OrderDTO
{
    public class OrderProductDTO
    {
        public ProductDTO.ProductDTO Product { get; set; }
        public int quantity { get; set; }
    }
}
