using System;
using System.Collections.Generic;

namespace Gecko.NCore.Client.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public interface IObjectModelAdapter
	{
		/// <summary>
		/// Queries the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="filterExpression">The filter expression.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <param name="takeCount">The take count.</param>
		/// <param name="skipCount">The skip count.</param>
		/// <returns></returns>
		IEnumerable<object> Query(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);

		/// <summary>
		/// Queries the count.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="filterExpression">The filter expression.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <returns></returns>
		int QueryCount(string dataObjectName, string filterExpression, string sortExpression);

		/// <summary>
		/// Storeds the query.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="queryId">The query id.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <param name="takeCount">The take count.</param>
		/// <param name="skipCount">The skip count.</param>
		/// <returns></returns>
		IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);

		/// <summary>
		/// Storeds the query count.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="queryId">The query id.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <returns></returns>
		int StoredQueryCount(string dataObjectName, int queryId, string sortExpression);

		/// <summary>
		/// Creates the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <returns></returns>
		object Create(string dataObjectName);

		/// <summary>
		/// Deletes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Delete(object dataObject);

		/// <summary>
		/// Updates the <paramref name="modifiedDataObjects"/> as a batch operation.
		/// </summary>
		/// <param name="modifiedDataObjects">The modified data objects.</param>
		/// <returns></returns>
		IEnumerable<object> BatchUpdate(IEnumerable<object> modifiedDataObjects);

		/// <summary>
		/// Inserts the <paramref name="newDataObjects"/> as a batch operation.
		/// </summary>
		/// <param name="newDataObjects">The new data objects.</param>
		/// <returns></returns>
		IEnumerable<object> BatchInsert(IEnumerable<object> newDataObjects);

		/// <summary>
		/// Finds the specified predicate.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="primaryKeys">The primary keys.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <returns></returns>
		IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects);

		/// <summary>
		/// Initializes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		/// <returns></returns>
		IDataObjectAccess Initialize(object dataObject);


		/// <summary>
		/// Gets the the data object base type .
		/// </summary>
		/// <value>The data object base type .</value>
		Type DataObjectBaseType { get; }

		/// <summary>
		/// Gets the custom field descriptor.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="primaryKeys">The primary keys.</param>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptor(string dataObjectName, IDictionary<string, string> primaryKeys, string category);
    }
}
