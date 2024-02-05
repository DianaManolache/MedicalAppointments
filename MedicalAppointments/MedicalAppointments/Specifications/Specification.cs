using MedicalAppointments.Base;
using MedicalAppointments.Data;
using System.Linq.Expressions;

namespace MedicalAppointments.Specifications
{
    public abstract class Specification<TEntity> where TEntity : BaseEntity
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<TEntity, bool>> criteria>) 
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public List<Expression<Func<TEntity, object>>>? Includes { get; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes?.Add(includeExpression);
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
    }
}
