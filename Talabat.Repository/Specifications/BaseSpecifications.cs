﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
            => Includes.Add(IncludeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
            => OrderByDesc = orderByDesc;

        protected void AddPagination(int skip , int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
        }
    }
}
