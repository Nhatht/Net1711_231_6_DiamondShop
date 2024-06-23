using DiamondShopData.Base;
using DiamondShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.DAO
{
	public class CompanyDAO : GenericRepository<Company>
	{
		public CompanyDAO(Net17112316DiamondShopContext context) : base(context)
		{
		}
	}
}