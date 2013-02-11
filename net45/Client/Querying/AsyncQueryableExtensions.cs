using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// Class AsyncQueryableExtensions
    /// </summary>
    public static class AsyncQueryableExtensions
    {
        /// <summary>
        /// Firsts the async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns>Task{``0}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// Firsts the async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task{``0}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// Firsts the or default async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns>Task{``0}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// Firsts the or default async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task{``0}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// Counts the async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns>Task{System.Int32}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// To the list async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns>Task{List{``0}}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// To the array async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns>Task{``0[]}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// Anies the async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <returns>Task{System.Boolean}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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

        /// <summary>
        /// Anies the async.
        /// </summary>
        /// <typeparam name="TElement">The type of the T element.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Task{System.Boolean}.</returns>
        /// <exception cref="System.ArgumentNullException">queryable</exception>
        /// <exception cref="System.NotSupportedException"></exception>
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
        /// <returns>Task{IEnumerable{``0}}.</returns>
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
