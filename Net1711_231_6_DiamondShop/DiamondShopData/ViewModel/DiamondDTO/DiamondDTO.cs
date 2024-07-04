using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.ViewModel.DiamondDTO
{
    public class DiamondDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Origin { get; set; } = null!;

        public decimal CaratWeight { get; set; }

        public string Color { get; set; } = null!;

        public string Clarity { get; set; } = null!;

        public string Cut { get; set; } = null!;

        public int CertificateNumber { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }
    }
}
