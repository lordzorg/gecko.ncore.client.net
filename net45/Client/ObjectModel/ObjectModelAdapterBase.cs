using System;
using System.Collections.Generic;

namespace Gecko.NCore.Client.ObjectModel
{
	/// <summary>
	///     Class ObjectModelAdapterBase
	/// </summary>
	/// <typeparam name="TServiceClient">The type of the T service client.</typeparam>
	public abstract class ObjectModelAdapterBase<TServiceClient> : ServiceAdapterBase<TServiceClient>, IObjectModelAdapter where TServiceClient : class, new()
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ObjectModelAdapterBase{TServiceClient}" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		protected ObjectModelAdapterBase(ClientSettings settings)
			: base(settings)
		{}

		/// <summary>
		///     Gets the the data object base type .
		/// </summary>
		/// <value>The data object base type .</value>
		public abstract Type DataObjectBaseType { get; }

		/// <summary>
		///     Queries the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="filterExpression">The filter expression.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <param name="takeCount">The take count.</param>
		/// <param name="skipCount">The skip count.</param>
		/// <returns>IEnumerable{System.Object}.</returns>
		public abstract IEnumerable<object> Query(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);

		/// <summary>
		///     Queries the count.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="filterExpression">The filter expression.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <returns>System.Int32.</returns>
		public abstract int QueryCount(string dataObjectName, string filterExpression, string sortExpression);

		/// <summary>
		///     Storeds the query.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="queryId">The query id.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <param name="takeCount">The take count.</param>
		/// <param name="skipCount">The skip count.</param>
		/// <returns>IEnumerable{System.Object}.</returns>
		public abstract IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);

		/// <summary>
		///     Storeds the query count.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="queryId">The query id.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <returns>System.Int32.</returns>
		public abstract int StoredQueryCount(string dataObjectName, int queryId, string sortExpression);

		/// <summary>
		///     Creates the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <returns>System.Object.</returns>
		public abstract object Create(string dataObjectName);

		/// <summary>
		///     Deletes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		public abstract void Delete(object dataObject);

		/// <summary>
		///     Updates the <paramref name="modifiedDataObjects" /> as a batch operation.
		/// </summary>
		/// <param name="modifiedDataObjects">The modified data objects.</param>
		/// <returns>IEnumerable{System.Object}.</returns>
		public abstract IEnumerable<object> BatchUpdate(IEnumerable<object> modifiedDataObjects);

		/// <summary>
		///     Inserts the <paramref name="newDataObjects" /> as a batch operation.
		/// </summary>
		/// <param name="newDataObjects">The new data objects.</param>
		/// <returns>IEnumerable{System.Object}.</returns>
		public abstract IEnumerable<object> BatchInsert(IEnumerable<object> newDataObjects);

		/// <summary>
		///     Finds the specified predicate.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="primaryKeys">The primary keys.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <returns>IDataObjectAccess.</returns>
		public abstract IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects);

		/// <summary>
		///     Initializes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		/// <returns>IDataObjectAccess.</returns>
		public abstract IDataObjectAccess Initialize(object dataObject);

		/// <summary>
		///     Gets the custom field descriptor.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="primaryKeys">The primary keys.</param>
		/// <param name="category">The category.</param>
		/// <returns>ICollection{ICustomFieldDescriptor}.</returns>
		public abstract ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptor(string dataObjectName, IDictionary<string, string> primaryKeys, string category);
	}
}