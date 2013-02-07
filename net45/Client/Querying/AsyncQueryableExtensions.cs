using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Querying
{
    public static class AsyncQueryableExtensions
    {
        public static async Task<TElement> FirstAsync<TElement>(this IQueryable<TElement> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof (AsyncQueryableExtensions), "FirstAsync", new[] {typeof (TElement)}, queryable.Expression);
            var result = await queryProvider.ExecuteAsync<TElement>(methodCallExpression);
            return result;
        }

        public static async Task<TElement> FirstAsync<TElement>(this IQueryable<TElement> queryable, Expression<Func<TElement, bool>> predicate)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof (AsyncQueryableExtensions), "FirstAsync", new[] {typeof (TElement)}, queryable.Expression, predicate);
            var result = await queryProvider.ExecuteAsync<TElement>(methodCallExpression);
            return result;
        }

        public static async Task<TElement> FirstOrDefaultAsync<TElement>(this IQueryable<TElement> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof (AsyncQueryableExtensions), "FirstOrDefaultAsync", new[] {typeof (TElement)}, queryable.Expression);
            var result = await queryProvider.ExecuteAsync<TElement>(methodCallExpression);
            return result;
        }

        public static async Task<TElement> FirstOrDefaultAsync<TElement>(this IQueryable<TElement> queryable, Expression<Func<TElement, bool>> predicate)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof (AsyncQueryableExtensions), "FirstOrDefaultAsync", new[] {typeof (TElement)}, queryable.Expression, predicate);
            var result = await queryProvider.ExecuteAsync<TElement>(methodCallExpression);
            return result;
        }

        public static async Task<int> CountAsync<TElement>(this IQueryable<TElement> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof(AsyncQueryableExtensions), "CountAsync", new[] { typeof(TElement) }, queryable.Expression);
            return await queryProvider.ExecuteAsync<int>(methodCallExpression);
        }

        public static async Task<List<TElement>> ToListAsync<TElement>(this IQueryable<TElement> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var result = await queryProvider.ExecuteAsync<IEnumerable<TElement>>(queryable.Expression);
            return result.ToList();
        }

        public static async Task<TElement[]> ToArrayAsync<TElement>(this IQueryable<TElement> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var result = await queryProvider.ExecuteAsync<IEnumerable<TElement>>(queryable.Expression);
            return result.ToArray();
        }

        public static async Task<bool> AnyAsync<TElement>(this IQueryable<TElement> queryable)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof (AsyncQueryableExtensions), "AnyAsync", new[] {typeof (TElement)}, queryable.Expression);
            return await queryProvider.ExecuteAsync<bool>(methodCallExpression);
        }

        public static async Task<bool> AnyAsync<TElement>(this IQueryable<TElement> queryable, Expression<Func<TElement, bool>> predicate)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var queryProvider = queryable.Provider as AsyncDataObjectQueryProvider;
            if (queryProvider == null)
                throw new NotSupportedException();

            var methodCallExpression = Expression.Call(typeof (AsyncQueryableExtensions), "AnyAsync", new[] {typeof (TElement)}, queryable.Expression, predicate);
            return await queryProvider.ExecuteAsync<bool>(methodCallExpression);
        }

        /// <summary>
        /// Lazies the specified queryable.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TElement>> LazyAsync<TElement>(this IQueryable<TElement> queryable)
        {
            return await queryable.LazyAsync(10);
        }

        /// <summary>
        /// Lazies the specified queryable.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TElement>> LazyAsync<TElement>(this IQueryable<TElement> queryable, int batchSize)
        {
            var combinedResult = new List<TElement>();
            var totalCount = queryable.Count();
            var skip = 0;
            while (skip < totalCount)
            {
                var batchQuery = queryable.Skip<TElement>(skip).Take<TElement>(batchSize);
                var batchQueryCount = 0;
                foreach (var result in await batchQuery.ToListAsync())
                {
                    combinedResult.Add(result);
                    batchQueryCount++;
                }

                skip += batchQueryCount;

                if (batchQueryCount < batchSize)
                {
                    return combinedResult;
                }
            }

            return combinedResult;
        }
    }
}
