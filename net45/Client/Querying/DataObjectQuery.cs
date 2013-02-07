using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// Provides support for querying entites via the Object Model Service.
    /// </summary>
    /// <typeparam name="TElement">The type of the entity.</typeparam>
    internal class DataObjectQuery<TElement> : IOrderedQueryable<TElement>
    {
        private readonly IQueryProvider _provider;
        private readonly Expression _expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectQuery{TElement}"/> class.
        /// </summary>
        /// <param name="provider">The query provider.</param>
        public DataObjectQuery(IQueryProvider provider)
        {
            _provider = provider;
            _expression = Expression.Constant(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectQuery{TElement}"/> class.
        /// </summary>
        /// <param name="provider">The query provider.</param>
        /// <param name="expression">The expression.</param>
        public DataObjectQuery(IQueryProvider provider, Expression expression)
        {
            _provider = provider;
            _expression = expression;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TElement> GetEnumerator()
        {
            var result = (IEnumerable)_provider.Execute(_expression);
            return result.Cast<TElement>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public Expression Expression
        {
            get { return _expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType
        {
            get { return typeof(TElement); }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider
        {
            get { return _provider; }
        }
    }
}
