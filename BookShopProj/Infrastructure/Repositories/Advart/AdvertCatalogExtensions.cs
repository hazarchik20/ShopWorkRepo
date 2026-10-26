using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Advart
{
    internal static class AdvertCatalogExtensions
    {
        public static IQueryable<Advert> ApplyCatalogFilters(
            this IQueryable<Advert> query, string? search, AdvertStateOfUse? stateOfUse)
        {
            query = query.Where(x => x.Status == AdvertStatus.Active && x.DeletedAt == null);
            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(x => x.Title.Contains(term) || x.Description.Contains(term));
            }
            if (stateOfUse.HasValue)
                query = query.Where(x => x.StateOfUse == stateOfUse.Value);
            return query;
        }

        public static IOrderedQueryable<Advert> ApplyCatalogSort(
            this IQueryable<Advert> query, string? sort) => sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price).ThenBy(x => x.Id),
                "priceDesc" => query.OrderByDescending(x => x.Price).ThenBy(x => x.Id),
                _ => query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id)
            };
    }
}


