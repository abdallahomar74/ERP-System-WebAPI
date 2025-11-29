using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity>(
            IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity> specifications) where TEntity : class
        {
            var query = inputQuery;

            if (specifications.criteria is not null)
                query = query.Where(specifications.criteria);

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions.Aggregate(
                    query,
                    (current, includeExp) => current.Include(includeExp)
                );
            }

            return query;
        }
    }
}
