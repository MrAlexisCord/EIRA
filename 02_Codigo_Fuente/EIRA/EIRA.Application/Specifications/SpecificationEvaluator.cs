﻿using EIRA.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EIRA.Application.Specifications
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {

                query = query.Where(specification.Criteria);
            }
            if (specification.Includes.Count > 0)
            {
                query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));
            }


            if (specification.IncludeStrings.Count > 0)
            {
                query = specification.IncludeStrings.Aggregate(query,

                                            (current, include) => current.Include(include));
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).Select(specification.GroupBySelect);
            }


            if (specification.Join != null)
            {
                // query = query.Join(specification.Join).SelectMany(x => x);
            }

            if (specification.IsActiveStateEnabled.HasValue)
            {
                query = query.Where(d => d.IsActive == specification.IsActiveStateEnabled.Value);
            }


            if (specification.IsPagingEnabled)
            {
                query = query.Skip((specification.Skip == 1 ? 0 : (specification.Skip - 1)) * specification.Take)
                                .Take(specification.Take);
            }

            if (specification.IsCustomsPagingEnabled)
            {
                specification.CustomInclude?.DynamicInvoke(query);
            }
            if (specification.IsDistinctEnabled)
            {
                if (specification.EqualityComparerDistinct is null)
                {
                    query = query.Distinct();
                }
                else
                {
                    query = query.Distinct(specification.EqualityComparerDistinct);
                }
            }

            return query;
        }

        public static async Task<IQueryable<T>> GetQuery(string procedure, DbSet<T> inicial, ISpecification<T> specification)
        {
            var nose = await inicial.FromSqlRaw(procedure, specification.SqlParameters.ToArray()).ToListAsync();
            var query = nose.AsQueryable();
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).Select(specification.GroupBySelect);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip((specification.Skip == 1 ? 0 : (specification.Skip - 1)) * specification.Take)
                                .Take(specification.Take);
            }

            if (specification.IsCustomsPagingEnabled)
            {
                specification.CustomInclude?.DynamicInvoke(query);
            }
            if (specification.IsDistinctEnabled)
            {
                if (specification.EqualityComparerDistinct is null)
                {
                    query = query.Distinct();
                }
                else
                {
                    query = query.Distinct(specification.EqualityComparerDistinct);
                }
            }

            return query;
        }
    }
}
