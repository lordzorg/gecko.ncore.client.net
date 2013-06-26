using System;
using System.Collections.Generic;
using System.Linq;
using Gecko.NCore.Client.Documents;
using Gecko.NCore.Client.Functions;
using Gecko.NCore.Client.Metadata;
using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client
{
	/// <summary>
	/// 
	/// </summary>
	public partial interface IEphorteContext : IDisposable
	{
		/// <summary>
		/// Adds the specified new data object.
		/// </summary>
		/// <param name="newDataObject">The new data object.</param>
		void Add(object newDataObject);

		/// <summary>
		/// Removes the specified new data object.
		/// </summary>
		/// <param name="dataObject">The new data object.</param>
		void Remove(object dataObject);

		/// <summary>
		/// Attaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Attach(object dataObject);

		/// <summary>
		/// Detaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Detach(object dataObject);

		/// <summary>
		/// Creates the type matching the specified <paramref name="dataObjectName"/>.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <returns></returns>
		object Create(string dataObjectName);

		/// <summary>
		/// Occurs when the <see cref="IEphorteContext"/> has saved changes.
		/// </summary>
		event EventHandler<SavedChangesEventArgs> SavedChanges;

		/// <summary>
		/// Occurs when the <see cref="IEphorteContext"/> is saving changes.
		/// </summary>
		event EventHandler<SavingChangesEventArgs> SavingChanges;

		/// <summary>
		/// Creates a <see cref="IQueryable{T}">queryable</see> that be used to query the ePhorte© Integration Services.
		/// </summary>
		/// <typeparam name="TDataObject">The type of the data object.</typeparam>
		/// <returns>A <see cref="IQueryable{TDataObject}"/> instance.</returns>
		IQueryable<TDataObject> Query<TDataObject>() where TDataObject : class;

		/// <summary>
		/// Queries the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <returns></returns>
		IQueryable Query(string dataObjectName);

		/// <summary>
		/// Gets the functions.
		/// </summary>
		/// <value>The functions.</value>
		IFunctionManager Functions { get; }

		/// <summary>
		/// Gets the documents.
		/// </summary>
		/// <value>The documents.</value>
		IDocumentManager Documents { get; }

		/// <summary>
		/// Gets the metadata.
		/// </summary>
		IMetadataManager Metadata { get; }

		/// <summary>
		/// Initializes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		/// <returns></returns>
		IDataObjectAccess Initialize(object dataObject);

		/// <summary>
		/// Finds the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="predicate">The predicate.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <returns></returns>
		IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> predicate, params string[] relatedObjects);

		/// <summary>
		/// Gets the custom field descriptors.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="primaryKeys">The primary keys.</param>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptors(string dataObjectName, IDictionary<string, string> primaryKeys, string category);

		/// <summary>
		/// Saves the changes.
		/// </summary>
		void SaveChanges();
	}
}