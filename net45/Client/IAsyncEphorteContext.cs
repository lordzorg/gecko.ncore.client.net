using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gecko.NCore.Client.Documents;
using Gecko.NCore.Client.Functions;
using Gecko.NCore.Client.Metadata;
using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAsyncEphorteContext: IEphorteContext
	{
		/// <summary>
		/// Gets the functions.
		/// </summary>
		/// <value>The functions.</value>
		new IAsyncFunctionManager Functions { get; }

		/// <summary>
		/// Gets the documents.
		/// </summary>
		/// <value>The documents.</value>
		new IAsyncDocumentManager Documents { get; }

		/// <summary>
		/// Gets the metadata.
		/// </summary>
		new IAsyncMetadataManager Metadata { get; }

        /// <summary>
        /// Initializes the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <returns></returns>
        Task<IDataObjectAccess> InitializeAsync(object dataObject);

        /// <summary>
        /// Finds the specified data object name.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="relatedObjects">The related objects.</param>
        /// <returns></returns>
        Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> predicate, params string[] relatedObjects);

        /// <summary>
        /// Gets the custom field descriptors.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="primaryKeys">The primary keys.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorsAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        Task SaveChangesAsync();
	}
}