using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;

namespace Talabat.Repository.Specifications
{
    public class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery (IQueryable<T> inputQuery , ISpecifications<T> specs)
        {
            var query = inputQuery;

            if (specs.Criteria != null)
                query = query.Where(specs.Criteria);

            if (specs.OrderBy != null)
                query = query.OrderBy(specs.OrderBy);

            if (specs.OrderByDesc != null)
                query = query.OrderByDescending(specs.OrderByDesc);

            if (specs.IsPaginated)
                query = query.Skip(specs.Skip).Take(specs.Take);

            query = specs.Includes.Aggregate(query , (current , include) => current.Include(include));

            return query;
        }
    }
}
