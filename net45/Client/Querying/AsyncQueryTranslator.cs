using System;
using System.Linq;
using System.Linq.Expressions;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// 
	/// </summary>
	internal class AsyncQueryTranslator: QueryTranslator
	{
		/// <summary>
		/// Visits the method call.
		/// </summary>
		/// <param name="methodCall">The method call.</param>
		/// <returns></returns>
		protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
            if (methodCall.Method.DeclaringType == typeof(AsyncQueryableExtensions))
                return VisitAsyncQueryableExtensionsMethodCall(methodCall);

		    return base.VisitMethodCall(methodCall);
		}

	    private Expression VisitAsyncQueryableExtensionsMethodCall(MethodCallExpression methodCall)
	    {
            switch (methodCall.Method.Name)
            {
                case "CountAsync":
                    return VisitCountMethodCall(methodCall);
                case "FirstAsync":
                    return VisitFirstMethodCall(methodCall);
                case "FirstOrDefaultAsync":
                    return VisitFirstOrDefaultMethodCall(methodCall);
                case "AnyAsync":
                    return VisitAnyMethodCall(methodCall);
                case "ToListAsync":
                    return VisitToListMethodCall(methodCall);
                case "ToArrayAsync":
                    return VisitToArrayMethodCall(methodCall);
                default:
                    throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
            }
        }

	    private Expression VisitToArrayMethodCall(MethodCallExpression methodCall)
	    {
            Visit(methodCall.Arguments[0]);
            return methodCall;

	    }

	    private Expression VisitToListMethodCall(MethodCallExpression methodCall)
	    {
            Visit(methodCall.Arguments[0]);
            return methodCall;
        }

	    /// <summary>
		/// Visits the constant.
		/// </summary>
		/// <param name="constant">The constant.</param>
		/// <returns></returns>
		protected override Expression VisitConstant(ConstantExpression constant)
		{
			var queryable = constant.Value as IQueryable;
			if (queryable != null)
			{
				DataObjectType = queryable.ElementType;
			}

			return constant;
		}
	}
}
