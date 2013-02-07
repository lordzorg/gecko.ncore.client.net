using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// This class contains extension methods that provide extra capabilities to <see cref="IQueryable{TElement}"/> instances.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Ensures that the member selected by <paramref name="memberSelector"/> is included in the <paramref name="queryable"/> execution result.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TMemberElementType">The type of the data object.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="memberSelector">The member selector.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static IQueryable<TElement> Include<TElement, TMemberElementType>(this IQueryable<TElement> queryable, Expression<Func<TElement, IDataObjectCollection<TMemberElementType>>> memberSelector)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var elementType = queryable.ElementType;
            var dataObjectType = typeof(TMemberElementType);
            var expression = Expression.Call(typeof(QueryableExtensions), "Include", new[] { elementType, dataObjectType }, queryable.Expression, memberSelector);
            var includeQueryable = queryable.Provider.CreateQuery<TElement>(expression);
            return includeQueryable;
        }

        /// <summary>
        /// Ensures that the member selected by <paramref name="memberSelector"/> is included in the <paramref name="queryable"/> execution result.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="memberSelector">The member selector.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static IQueryable<TElement> Include<TElement>(this IQueryable<TElement> queryable, Expression<Func<TElement, object>> memberSelector)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var elementType = queryable.ElementType;
            var expression = Expression.Call(typeof(QueryableExtensions), "Include", new[] { elementType }, queryable.Expression, memberSelector);
            var includeQueryable = queryable.Provider.CreateQuery<TElement>(expression);
            return includeQueryable;
        }

        /// <summary>
        /// Ensures that the member selected by <paramref name="memberSelector"/> is included in the <paramref name="queryable"/> execution result.
        /// </summary>
        /// <param name="queryable">The queryable.</param>
        /// <param name="memberSelector">The member selector.</param>
        /// <returns></returns>
        public static IQueryable Include(this IQueryable queryable, string memberSelector)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            if (string.IsNullOrEmpty(memberSelector))
                throw new ArgumentNullException("memberSelector");

            var elementType = queryable.ElementType;

            var propertyInfos = new List<PropertyInfo>();

            var declaringType = elementType;
            foreach (var memberName in memberSelector.Split('.'))
            {
                var propertyInfo = declaringType.GetProperty(memberName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException(string.Format(Resources.MemberNotFound, memberName, declaringType.Name));
                }
                declaringType = propertyInfo.PropertyType;
                propertyInfos.Add(propertyInfo);
            }

            return IncludingQueryable(queryable, elementType, propertyInfos);
        }

    	private static IQueryable IncludingQueryable(IQueryable queryable, Type elementType, IList<PropertyInfo> properties)
    	{
    		Type delegateType;
    		var typeArguments = new List<Type> {elementType};

    		var lastProperty = properties.Last();
    		if (typeof (IDataObjectCollection).IsAssignableFrom(lastProperty.PropertyType))
    		{
    			typeArguments.Add(lastProperty.PropertyType.GetGenericArguments()[0]);
    			delegateType = Expression.GetFuncType(elementType, lastProperty.PropertyType);
    		}
    		else
    		{
    			delegateType = Expression.GetFuncType(elementType, typeof (object));
    		}

    		var lambdaParameter = Expression.Parameter(elementType, "x");
    		var lambdaBody = Expression.MakeMemberAccess(lambdaParameter, properties.First());
    		lambdaBody = properties.Skip(1).Aggregate(lambdaBody, Expression.MakeMemberAccess);

    		var lambdaExpression = Expression.Lambda(delegateType, lambdaBody, lambdaParameter);

    		var expression = Expression.Call(typeof (QueryableExtensions), "Include", typeArguments.ToArray(), queryable.Expression, lambdaExpression);
    		var includeQueryable = queryable.Provider.CreateQuery(expression);
    		return includeQueryable;
    	}

    	/// <summary>
        /// Ensures that the member selected by <paramref name="memberSelector"/> is included in the <paramref name="queryable"/> execution result.
        /// </summary>
        /// <param name="queryable">The queryable.</param>
        /// <param name="memberSelector">The member selector.</param>
        /// <returns></returns>
        public static IQueryable<TElement> Include<TElement>(this IQueryable<TElement> queryable, string memberSelector)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            if (string.IsNullOrEmpty(memberSelector))
                throw new ArgumentNullException("memberSelector");

            var elementType = queryable.ElementType;

            var propertyInfos = new List<PropertyInfo>();

            var declaringType = elementType;
            foreach (var memberName in memberSelector.Split('.'))
            {
                var propertyInfo = declaringType.GetProperty(memberName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException(string.Format(Resources.MemberNotFound, memberName, declaringType.Name));
                }
                declaringType = propertyInfo.PropertyType;
                propertyInfos.Add(propertyInfo);
            }

            Type delegateType;
            var typeArguments = new List<Type> { elementType };

            var lastPropertyType = propertyInfos.Last().PropertyType;
            if (typeof(IDataObjectCollection).IsAssignableFrom(lastPropertyType))
            {
                typeArguments.Add(lastPropertyType.GetGenericArguments()[0]);
                delegateType = Expression.GetFuncType(elementType, lastPropertyType);
            }
            else
            {
                delegateType = Expression.GetFuncType(elementType, typeof(object));
            }

            var lambdaParameter = Expression.Parameter(elementType, "x");
            var lambdaBody = Expression.MakeMemberAccess(lambdaParameter, propertyInfos[0]);
            for (var propertyInfoIndex = 1; propertyInfoIndex < propertyInfos.Count; propertyInfoIndex++)
            {
                lambdaBody = Expression.MakeMemberAccess(lambdaBody, propertyInfos[propertyInfoIndex]);
            }

            var lambdaExpression = Expression.Lambda(delegateType, lambdaBody, lambdaParameter);

            var expression = Expression.Call(typeof(QueryableExtensions), "Include", typeArguments.ToArray(), queryable.Expression, lambdaExpression);
            var includeQueryable = queryable.Provider.CreateQuery<TElement>(expression);
            return includeQueryable;
        }

        /// <summary>
        /// Lazies the specified queryable.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns></returns>
        public static IEnumerable<TElement> Lazy<TElement>(this IQueryable<TElement> queryable)
        {
            return queryable.Lazy(10);
        }

        /// <summary>
        /// Lazies the specified queryable.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <returns></returns>
        public static IEnumerable<TElement> Lazy<TElement>(this IQueryable<TElement> queryable, int batchSize)
        {
            var totalCount = Queryable.Count(queryable);
            var skip = 0;
            while (skip < totalCount)
            {
                var batchQuery = Queryable.Take(Queryable.Skip(queryable, skip), batchSize);
                var batchQueryCount = 0;
                foreach (var result in batchQuery)
                {
                    yield return result;
                    batchQueryCount++;
                }

                skip += batchQueryCount;

                if (batchQueryCount < batchSize)
                    yield break;
            }
        }
    }
}
