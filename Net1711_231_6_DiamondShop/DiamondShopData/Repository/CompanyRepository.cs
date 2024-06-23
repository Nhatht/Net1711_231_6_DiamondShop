using DiamondShopData.Base;
using DiamondShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.Repository
{
	public class CompanyRepository : GenericRepository<Company>
	{
		public CompanyRepository(Net17112316DiamondShopContext context) : base(context)
		{
		}
	}
}