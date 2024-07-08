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
    public class DiamondRepository : GenericRepository<Diamond>
    {
        public DiamondRepository(Net17112316DiamondShopContext context) : base(context)
        {

        }
        public async Task<PageableResponseDTO<Diamond>> GetAllAsync(int pageNumber, int pageSize, string? query = null)
        {
            IQueryable<Diamond> queryable = _context.Set<Diamond>();

            if (!string.IsNullOrEmpty(query))
            {
                var filters = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (filters.Length != 0)
                {
                    var origin = await _context.Set<Diamond>().Select(p => p.Origin).Distinct().ToListAsync();

                    foreach (var filter in filters)
                    {
                        var trimmedFilter = filter.Trim();

                        // Try to parse as price first
                        if (Decimal.TryParse(trimmedFilter, out decimal price))
                        {
                            queryable = queryable.Where(BuildPredicate("Price", trimmedFilter));
                        }
                        else
                        {
                            // Check if it is a metal value from the database
                            if (origin.Contains(trimmedFilter, StringComparer.OrdinalIgnoreCase))
                            {
                                queryable = queryable.Where(BuildPredicate("Origin", trimmedFilter));
                            }
                            else
                            {
                                // Otherwise, assume it's a name filter
                                queryable = queryable.Where(BuildPredicate("Name", trimmedFilter));
                            }
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
            return new PageableResponseDTO<Diamond>()
            {
                List = list.ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalOfPages = totalOfPages
            };
        }

        private static Expression<Func<Diamond, bool>> BuildPredicate(string property, string value)
        {
            var parameter = Expression.Parameter(typeof(Diamond), "x");
            var member = Expression.PropertyOrField(parameter, property);

            if (member.Type == typeof(string))
            {
                var constant = Expression.Constant(value);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(member, containsMethod, constant);
                return Expression.Lambda<Func<Diamond, bool>>(containsExpression, parameter);
            }
            else if (member.Type == typeof(Decimal))
            {
                var intValue = Decimal.Parse(value);
                var equalExpression = Expression.Equal(member, Expression.Constant(intValue));
                return Expression.Lambda<Func<Diamond, bool>>(equalExpression, parameter);
            }
            else if (member.Type == typeof(DateTime))
            {
                var dateTimeValue = DateTime.Parse(value);
                var equalExpression = Expression.Equal(member, Expression.Constant(dateTimeValue));
                return Expression.Lambda<Func<Diamond, bool>>(equalExpression, parameter);
            }

            throw new NotSupportedException($"The type of {property} is not supported");
        }
    }
}
