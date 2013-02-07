using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAsyncObjectModelAdapter: IObjectModelAdapter
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
        Task<IEnumerable<object>> QueryAsync(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);

        /// <summary>
        /// Queries the count.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        Task<int> QueryCountAsync(string dataObjectName, string filterExpression, string sortExpression);

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
        Task<IEnumerable<object>> StoredQueryAsync(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);

        /// <summary>
        /// Storeds the query count.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="queryId">The query id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        Task<int> StoredQueryCountAsync(string dataObjectName, int queryId, string sortExpression);

        /// <summary>
        /// Deletes the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        Task DeleteAsync(object dataObject);

        /// <summary>
        /// Updates the <paramref name="modifiedDataObjects"/> as a batch operation.
        /// </summary>
        /// <param name="modifiedDataObjects">The modified data objects.</param>
        /// <returns></returns>
        Task<IEnumerable<object>> BatchUpdateAsync(IEnumerable<object> modifiedDataObjects);

        /// <summary>
        /// Inserts the <paramref name="newDataObjects"/> as a batch operation.
        /// </summary>
        /// <param name="newDataObjects">The new data objects.</param>
        /// <returns></returns>
        Task<IEnumerable<object>> BatchInsertAsync(IEnumerable<object> newDataObjects);

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="primaryKeys">The primary keys.</param>
        /// <param name="relatedObjects">The related objects.</param>
        /// <returns></returns>
        Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects);

        /// <summary>
        /// Initializes the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <returns></returns>
        Task<IDataObjectAccess> InitializeAsync(object dataObject);

        /// <summary>
        /// Gets the custom field descriptor.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="primaryKeys">The primary keys.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category);
    }
}
