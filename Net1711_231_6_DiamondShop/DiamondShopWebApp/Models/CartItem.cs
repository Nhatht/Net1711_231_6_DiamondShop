using DiamondShopData.Models;
using DiamondShopData.ViewModel.ProductDTO;

namespace DiamondShopWebApp.Models
{
    public class CartItem
    {
        public int quantity { get; set; }
        public ProductDTO product { get; set; }
    }
}
