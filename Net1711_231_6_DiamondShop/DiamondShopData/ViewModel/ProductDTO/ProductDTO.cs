using DiamondShopData.ViewModel.DiamondDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.ViewModel.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public int DiamondId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Metal { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public long Size { get; set; }

        public int Stock { get; set; }

        public bool IsDeleted { get; set; }
        public string DiamondName { get; set; } = null!;

        public DiamondProductDTO Diamond { get; set; } = null!;
    }
}
