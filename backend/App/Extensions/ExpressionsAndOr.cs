using System.Linq.Expressions;

namespace App.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = expr1.Parameters[0];

            var body = Expression.AndAlso(
                expr1.Body,
                ExpressionVisitorReplace(expr2.Body, expr2.Parameters[0], parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = expr1.Parameters[0];

            var body = Expression.OrElse(
                expr1.Body,
                ExpressionVisitorReplace(expr2.Body, expr2.Parameters[0], parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression ExpressionVisitorReplace(
            Expression body,
            ParameterExpression source,
            ParameterExpression target)
        {
            return new ReplaceParameterVisitor(source, target).Visit(body);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _source;
            private readonly ParameterExpression _target;

            public ReplaceParameterVisitor(ParameterExpression source, ParameterExpression target)
            {
                _source = source;
                _target = target;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _source ? _target : base.VisitParameter(node);
            }
        }
    }
}