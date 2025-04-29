using Merchandise.Domain.DataModels.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Merchandise.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<SearchResultDataModel<T>> ToPaginatedResultAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new SearchResultDataModel<T>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };
        }
    }
}
