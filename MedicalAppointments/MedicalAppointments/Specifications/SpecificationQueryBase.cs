using MedicalAppointments.Base;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointments.Specifications
{
    public static class SpecificationQueryBase
    {
        public static IQueryable<TEntity> Where<TEntity>(
            this IQueryable<TEntity> inputQuery,
            Specification<TEntity> specification)
            where TEntity : BaseEntity
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.Includes != null)
            {
                query = specification.Includes.Aggregate(query,
                                       (current, include) => current.Include(include));
            }
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            return query;
        }
    }
}
