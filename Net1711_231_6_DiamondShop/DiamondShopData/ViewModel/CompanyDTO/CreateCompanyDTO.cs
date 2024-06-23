using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.ViewModel.CompanyDTO
{
	public class CreateCompanyDTO
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Description { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
