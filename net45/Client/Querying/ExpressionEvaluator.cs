// See http://blogs.msdn.com/b/mattwar/archive/2007/08/01/linq-building-an-iqueryable-provider-part-iii.aspx

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ExpressionEvaluator
    {
        /// <summary>
        /// Performs evaluation and replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="canBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression PartialEval(Expression expression, Func<Expression, bool> canBeEvaluated)
        {
            return new SubtreeEvaluator(new Nominator(canBeEvaluated).Nominate(expression)).Eval(expression);
        }

        /// <summary>
        /// Performs evaluation and replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, CanBeEvaluatedLocally);
        }

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            if (IsRootQuery(expression))
                return false;

            if (IsQueryContext(expression))
                return false;

            return expression.NodeType != ExpressionType.Parameter;
        }

        private static bool IsQueryContext(Expression expression)
        {
            return expression.NodeType == ExpressionType.MemberAccess && ((MemberExpression)expression).Member.DeclaringType == typeof(QueryContext);
        }

        private static bool IsRootQuery(Expression expression)
        {
            if (expression.NodeType != ExpressionType.Constant)
                return false;

            var constantExpression = (ConstantExpression) expression;

            if (constantExpression.Value == null)
                return false;

            var constantType = constantExpression.Value.GetType();
            if (!constantType.IsGenericType || constantType.IsGenericTypeDefinition)
                return false;

            return constantType.GetGenericTypeDefinition() == typeof(DataObjectQuery<>);
        }

        /// <summary>
        /// Evaluates and replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        class SubtreeEvaluator : ExpressionVisitor
        {
            private readonly HashSet<Expression> _expressionCandidates;

            internal SubtreeEvaluator(HashSet<Expression> expressionCandidates)
            {
                _expressionCandidates = expressionCandidates;
            }

            internal Expression Eval(Expression exp)
            {
                return Visit(exp);
            }

            protected internal override Expression Visit(Expression exp)
            {
                if (exp == null)
                    return null;

                return _expressionCandidates.Contains(exp) ? Evaluate(exp) : base.Visit(exp);
            }

            private static Expression Evaluate(Expression e)
            {
                if (e.NodeType == ExpressionType.Constant)
                {
                    return e;
                }

                var lambda = Expression.Lambda(e);
                var lambdaDelegate = lambda.Compile();
                return Expression.Constant(lambdaDelegate.DynamicInvoke(null), e.Type);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        class Nominator : ExpressionVisitor
        {
            private readonly Func<Expression, bool> _canBeEvaluated;
            private HashSet<Expression> _expressionCandidates;
            private bool _cannotBeEvaluated;

            internal Nominator(Func<Expression, bool> canBeEvaluated)
            {
                _canBeEvaluated = canBeEvaluated;
            }
            
            internal HashSet<Expression> Nominate(Expression expression)
            {
                _expressionCandidates = new HashSet<Expression>();
                Visit(expression);
                return _expressionCandidates;
            }

            protected internal override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    var saveCannotBeEvaluated = _cannotBeEvaluated;
                    _cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!_cannotBeEvaluated)
                    {
                        if (_canBeEvaluated(expression))
                        {
                            _expressionCandidates.Add(expression);
                        }
                        else
                        {
                            _cannotBeEvaluated = true;
                        }
                    }
                    _cannotBeEvaluated |= saveCannotBeEvaluated;
                }

                return expression;
            }
        }
    }
}
