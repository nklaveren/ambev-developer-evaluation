using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    private readonly Expression<Func<T, bool>> _expression;

    protected BaseSpecification(Expression<Func<T, bool>> expression) => _expression = expression;

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = _expression.Compile();
        return predicate(entity);
    }

    public Expression<Func<T, bool>> ToExpression() => _expression;

}