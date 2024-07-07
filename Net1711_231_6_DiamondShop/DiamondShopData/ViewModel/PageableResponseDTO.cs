using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.ViewModel
{
    public class PageableResponseDTO<T>
    {
        public List<T> List { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalOfPages { get; set; }
    }
}
