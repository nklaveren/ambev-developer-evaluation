using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();
}