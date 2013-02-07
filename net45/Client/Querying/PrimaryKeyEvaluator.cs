using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
	internal static class PrimaryKeyEvaluator
	{
		/// <summary>
		/// Evaluates the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static IDictionary<string, string> Evaluate(Expression predicate)
		{
			var primaryKeys = new Dictionary<string, string>();
			var keySelectorVisitor = new KeySelectorVisitor(primaryKeys);
			keySelectorVisitor.Visit(ExpressionEvaluator.PartialEval(predicate));
			return primaryKeys;
		}

		/// <summary>
		/// Evaluates the specified expression.
		/// </summary>
		/// <param name="predicate">The expression.</param>
		/// <returns></returns>
		public static IDictionary<string, string> Evaluate<T>(Expression<Func<T, bool>> predicate)
		{
			var primaryKeys = new Dictionary<string, string>();
			var keySelectorVisitor = new KeySelectorVisitor(primaryKeys);
			keySelectorVisitor.Visit(ExpressionEvaluator.PartialEval(predicate));
			return primaryKeys;
		}

		private class KeySelectorVisitor: ExpressionVisitor
		{
			private readonly IDictionary<string, string> _primaryKeys;

			public KeySelectorVisitor(IDictionary<string, string> primaryKeys)
			{
				_primaryKeys = primaryKeys;
			}

			protected override Expression VisitBinary(BinaryExpression node)
			{
				switch (node.NodeType)
				{
					case ExpressionType.Equal:
						var constantExpression = node.Right as ConstantExpression;
						if (constantExpression == null)
							throw new NotSupportedException(Resources.KeySelectorVisitor_VisitBinary_The_right_operand_of_a_binary_expression_must_be_a_constant);

						var primaryKey = MemberEvaluator.Evaluate(node.Left);
						_primaryKeys.Add(primaryKey, constantExpression.Value == null ? "@" : constantExpression.Value.ToString());
						return node;
					case ExpressionType.And:
						return base.VisitBinary(node);
					case ExpressionType.AndAlso:
						return base.VisitBinary(node);
					default:
						throw new NotSupportedException(string.Format(Resources.KeySelectorVisitor_VisitBinary_Expressions_of_type_0_are_not_supported_in_primary_key_predicates, node.NodeType));
				}
			}

			protected override Expression VisitUnary(UnaryExpression node)
			{
				if (node.NodeType == ExpressionType.Convert)
					return node.Operand;

				return base.VisitUnary(node);
			}
		}
	}
}
