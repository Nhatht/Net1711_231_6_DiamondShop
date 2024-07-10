using DiamondShopData.Base;
using DiamondShopData.Models;
using DiamondShopData.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData.Repository
{
	public class CompanyRepository : GenericRepository<Company>
	{
		public CompanyRepository(Net17112316DiamondShopContext context) : base(context)
		{
		}
		public async Task<PageableResponseDTO<Company>> GetAllAsync(int pageNumber, int pageSize, string? query = null)
		{
			IQueryable<Company> queryable = _context.Set<Company>().Where(c => c.IsDeleted == false);

			if (!string.IsNullOrEmpty(query))
			{
				var filters = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (filters.Length != 0)
				{
					foreach (var filter in filters)
					{
						var trimmedFilter = filter.Trim();
						queryable = queryable.Where(BuildPredicate("Name", trimmedFilter))
											 .Union(queryable.Where(BuildPredicate("Description", trimmedFilter)))
											 .Union(queryable.Where(BuildPredicate("Address", trimmedFilter)))
											 .Union(queryable.Where(BuildPredicate("Email", trimmedFilter)));
					}
				}
			}
			var totalItemCount = await queryable.CountAsync();
			var totalOfPages = (int)Math.Ceiling((double)totalItemCount / pageSize);
			var list = await queryable.OrderBy(p => p.Id)
								.Skip((pageNumber - 1) * pageSize)
								.Take(pageSize)
							   .ToListAsync();
			return new PageableResponseDTO<Company>()
			{
				List = list.ToList(),
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalOfPages = totalOfPages
			};
		}
		private static Expression<Func<Company, bool>> BuildPredicate(string property, string value)
		{
			var parameter = Expression.Parameter(typeof(Company), "x");
			var member = Expression.PropertyOrField(parameter, property);

			if (member.Type == typeof(string))
			{
				var constant = Expression.Constant(value);
				var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
				var containsExpression = Expression.Call(member, containsMethod, constant);
				return Expression.Lambda<Func<Company, bool>>(containsExpression, parameter);
			}
			throw new NotSupportedException($"The type of {property} is not supported");
		}
	}
}