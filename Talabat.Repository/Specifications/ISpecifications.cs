using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
         Expression<Func<T , bool>> Criteria { get; }
         List<Expression<Func<T , object>>> Includes { get; }
         Expression<Func<T, object>> OrderBy { get; }
         Expression<Func<T, object>> OrderByDesc { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
