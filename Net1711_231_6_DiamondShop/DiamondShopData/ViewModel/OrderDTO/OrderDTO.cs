using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.ViewModel.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }


        public string Payment { get; set; }

        public string Status { get; set; } = null!;

        public DateOnly CreatedDate { get; set; }

        public long TotalPrice { get; set; }
    }
}
