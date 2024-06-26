﻿using DiamondShopData.ViewModel.ProductDTO;
using DiamondShopData.Base;
using DiamondShopData.DAO;
using DiamondShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiamondShopData.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public readonly Net17112316DiamondShopContext _context;
        public ProductRepository(Net17112316DiamondShopContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync(string? query = null)
        {
            IQueryable<Product> queryable = _context.Set<Product>();

            if (!string.IsNullOrEmpty(query))
            {
                var filters = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (filters.Length != 0)
                {
                    var metalValues = await _context.Set<Product>().Select(p => p.Metal).Distinct().ToListAsync();

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
                            if (metalValues.Contains(trimmedFilter, StringComparer.OrdinalIgnoreCase))
                            {
                                queryable = queryable.Where(BuildPredicate("Metal", trimmedFilter));
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

            return await queryable.ToListAsync();
        }

        private static Expression<Func<Product, bool>> BuildPredicate(string property, string value)
        {
            var parameter = Expression.Parameter(typeof(Product), "x");
            var member = Expression.PropertyOrField(parameter, property);

            if (member.Type == typeof(string))
            {
                var constant = Expression.Constant(value);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(member, containsMethod, constant);
                return Expression.Lambda<Func<Product, bool>>(containsExpression, parameter);
            }
            else if (member.Type == typeof(Decimal))
            {
                var intValue = Decimal.Parse(value);
                var equalExpression = Expression.Equal(member, Expression.Constant(intValue));
                return Expression.Lambda<Func<Product, bool>>(equalExpression, parameter);
            }
            else if (member.Type == typeof(DateTime))
            {
                var dateTimeValue = DateTime.Parse(value);
                var equalExpression = Expression.Equal(member, Expression.Constant(dateTimeValue));
                return Expression.Lambda<Func<Product, bool>>(equalExpression, parameter);
            }

            throw new NotSupportedException($"The type of {property} is not supported");
        }
    }
}
