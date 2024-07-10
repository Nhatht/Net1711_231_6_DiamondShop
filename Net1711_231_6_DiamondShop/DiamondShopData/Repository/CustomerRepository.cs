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
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(Net17112316DiamondShopContext context) : base(context)
        {
        }
        public async Task<PageableResponseDTO<Customer>> GetAllAsync(int pageNumber, int pageSize, string? query = null)
        {
            IQueryable<Customer> queryable = _context.Set<Customer>();

            if (!string.IsNullOrEmpty(query))
            {
                var filters = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (filters.Length != 0)
                {
                    var origin = await _context.Set<Customer>().Select(p => p.Name).Distinct().ToListAsync();

                    foreach (var filter in filters)
                    {
                        var trimmedFilter = filter.Trim();

                            // Check if it is a gender value from the database
                            if (origin.Contains(trimmedFilter, StringComparer.OrdinalIgnoreCase))
                            {
                                queryable = queryable.Where(BuildPredicate("Gender", trimmedFilter));
                            }
                            else
                            {
                                // Otherwise, assume it's a name filter
                                queryable = queryable.Where(BuildPredicate("Address", trimmedFilter));
                            }
                        
                    }
                }
            }
            var totalItemCount = await queryable.CountAsync();
            var totalOfPages = (int)Math.Ceiling((double)totalItemCount / pageSize);
            var list = await queryable.OrderBy(p => p.Id) // Assuming you're ordering by the Id. Adjust accordingly.
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                               .ToListAsync();
            return new PageableResponseDTO<Customer>()
            {
                List = list.ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalOfPages = totalOfPages
            };
        }

        private static Expression<Func<Customer, bool>> BuildPredicate(string property, string value)
        {
            var parameter = Expression.Parameter(typeof(Customer), "x");
            var member = Expression.PropertyOrField(parameter, property);

            if (member.Type == typeof(string))
            {
                var constant = Expression.Constant(value);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(member, containsMethod, constant);
                return Expression.Lambda<Func<Customer, bool>>(containsExpression, parameter);
            }

            throw new NotSupportedException($"The type of {property} is not supported");
        }

    }
}
