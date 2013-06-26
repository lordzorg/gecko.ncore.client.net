using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gecko.NCore.Client.Documents;
using Gecko.NCore.Client.Functions;
using Gecko.NCore.Client.Metadata;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Properties;
using Gecko.NCore.Client.Querying;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client
{
	/// <summary>
	/// Data access context that provide synchronous access to the ePhorte© Integration Services.
	/// </summary>
	public partial class EphorteContext : IEphorteContext
	{
		private readonly IStateManager _stateManager;
		private readonly IFunctionManager _functionManager;
		private readonly IDocumentManager _documentManager;
		private readonly IMetadataManager _metadataManager;
		private readonly IObjectModelAdapter _objectModelAdapter;
		protected readonly NcoreVersion NcoreVersion;

		public EphorteContext(
			IObjectModelAdapter objectModelAdapter = null,
			IFunctionsAdapter functionsAdapter = null,
			IDocumentsAdapter documentsAdapter = null,
			IMetadataAdapter metadataAdapter = null,
			NcoreVersion ncoreVersion = null
			) : this(CreateStateManager, objectModelAdapter, functionsAdapter, documentsAdapter, metadataAdapter, ncoreVersion)
		{ }

		private static StateManager CreateStateManager(IObjectModelAdapter objectModelAdapter, NcoreVersion ncoreVersion)
		{
			return objectModelAdapter == null
				? null
				: new StateManager(@this => new DataObjectQueryProvider(@this, objectModelAdapter, ncoreVersion));
		}

		private EphorteContext(
			Func<IObjectModelAdapter, NcoreVersion, IStateManager> createStateManager, // Workaround to be able to instantiate a statemanager with a different IQueryProvider
			IObjectModelAdapter objectModelAdapter, 
			IFunctionsAdapter functionsAdapter, 
			IDocumentsAdapter documentsAdapter, 
			IMetadataAdapter metadataAdapter, 
			NcoreVersion ncoreVersion
			)
		{
			NcoreVersion = ncoreVersion ?? NcoreVersion.Configured;

			if (objectModelAdapter != null)
			{
				_stateManager = createStateManager(objectModelAdapter, NcoreVersion);
				_objectModelAdapter = objectModelAdapter;
			}

			if (functionsAdapter != null)
				_functionManager = new FunctionManager(functionsAdapter);

			if (documentsAdapter != null)
				_documentManager = new DocumentManager(documentsAdapter);

			if (metadataAdapter != null)
				_metadataManager = new MetadataManager(metadataAdapter);
		}

		public IFunctionManager Functions
		{
			get
			{
				if(_functionManager == null)
					throw new NotSupportedException(Resources.EphorteContext_Functions_Function_access_functionality_requires_the_IFunctionsAdapter_to_be_provided);

				return _functionManager;
			}
		}

		public IDocumentManager Documents
		{
			get
			{
				if(_documentManager == null)
					throw new NotSupportedException(Resources.EphorteContext_Documents_Document_access_functionality_requires_the_IDocumentsAdapter_to_be_provided);
				return _documentManager;
			}
		}

		public IMetadataManager Metadata
		{
			get
			{
				if(_metadataManager == null)
					throw new NotSupportedException(Resources.EphorteContext_Metadata_Metadata_access_functionality_requires_the_IMetadataAdapter_to_be_provided);

				return _metadataManager;
			}
		}

		public IDataObjectAccess Initialize(object dataObject)
		{
			var dataObjectAccess = _objectModelAdapter.Initialize(dataObject);
			RefreshDataObject(dataObject, dataObjectAccess.DataObject);
			return dataObjectAccess;
		}

		public IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects)
		{
			var dataObjectAccess = _objectModelAdapter.Find(dataObjectName, primaryKeys, relatedObjects);
			StateManager.WeakAttach(dataObjectAccess.DataObject);
			return dataObjectAccess;
		}

		public ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptors(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
		{
			return _objectModelAdapter.GetCustomFieldDescriptor(dataObjectName, primaryKeys, category);
		}

		public void SaveChanges()
		{
			OnSavingChanges(new SavingChangesEventArgs(StateManager));
			foreach (var stateEntryGroup in StateManager.Entries.GroupBy(x => x.State).ToList())
			{
				var stateEntries = stateEntryGroup.ToList();
				switch (stateEntryGroup.Key)
				{
					case DataObjectState.Added:
						InsertDataObjects(stateEntries);
						break;
					case DataObjectState.Modified:
						UpdateDataObjects(stateEntries);
						break;
					case DataObjectState.Removed:
						DeleteDataObjects(stateEntries);
						break;
				}
			}
			OnSavedChanges(new SavedChangesEventArgs(StateManager));
		}

		private void DeleteDataObjects(IEnumerable<StateEntry> stateEntries)
		{
			foreach (var stateEntry in stateEntries)
			{
				_objectModelAdapter.Delete(stateEntry.DataObject);
				StateManager.Detach(stateEntry.DataObject);
			}
		}

		private void UpdateDataObjects(ICollection<StateEntry> stateEntries)
		{
			var updatedDataObjects = _objectModelAdapter.BatchUpdate(stateEntries.Select(x => x.DataObject));

			var stateEntryEnumerator = stateEntries.GetEnumerator();
			var updatedDataObjectEnumerator = updatedDataObjects.GetEnumerator();
			while (stateEntryEnumerator.MoveNext() && updatedDataObjectEnumerator.MoveNext())
			{
				var stateEntry = stateEntryEnumerator.Current;
				if (stateEntry == null)
					throw new InvalidOperationException();

				var modifiedDataObject = stateEntry.DataObject;
				var updatedDataObject = updatedDataObjectEnumerator.Current;
				RefreshDataObject(modifiedDataObject, updatedDataObject);
				stateEntry.MarkAsClean();
			}
		}

		private void InsertDataObjects(ICollection<StateEntry> stateEntries)
		{
			var insertedDataObjects = _objectModelAdapter.BatchInsert(stateEntries.Select(x => x.DataObject));
			var stateEntryEnumerator = stateEntries.GetEnumerator();
			var insertedDataObjectEnumerator = insertedDataObjects.GetEnumerator();
			while (stateEntryEnumerator.MoveNext() && insertedDataObjectEnumerator.MoveNext())
			{
				var stateEntry = stateEntryEnumerator.Current;
				if (stateEntry == null)
					throw new InvalidOperationException();
				
				var newDataObjects = stateEntry.DataObject;
				var insertedDataObject = insertedDataObjectEnumerator.Current;
				RefreshDataObject(newDataObjects, insertedDataObject);
				stateEntry.MarkAsClean();
			}
		}

		private void RefreshDataObject(object target, object source)
		{
			var targetType = target.GetType();
			var sourceType = source.GetType();

			if (targetType != sourceType)
				throw new InvalidOperationException();
			
			foreach (var property in targetType.GetProperties())
			{
				if (_objectModelAdapter.DataObjectBaseType.IsAssignableFrom(property.PropertyType))
					continue;

				var elementType = TypeSystem.ResolveElementType(property.PropertyType);
				if (elementType != null && _objectModelAdapter.DataObjectBaseType.IsAssignableFrom(elementType))
					continue;

				property.SetValue(target, property.GetValue(source, null), null);
			}
		}

		protected IStateManager StateManager
		{
			get
			{
				if (_stateManager == null)
					throw new NotSupportedException(Resources.EphorteContext_StateManager_Any_statemanagement_requires_the_IObjectModelAdapter_to_be_provided);

				return _stateManager;
			}
		}

		public IQueryable<TDataObject> Query<TDataObject>() where TDataObject : class
		{
			if (_objectModelAdapter == null)
				throw new NotSupportedException("Querying functionality requires the IObjectModelADapter to be provided");

			return new DataObjectQuery<TDataObject>(StateManager.QueryProvider);
		}

		public IQueryable Query(string dataObjectName)
		{
			if (string.IsNullOrEmpty(dataObjectName))
				throw new ArgumentNullException("dataObjectName");

			Type dataObjectBaseType = _objectModelAdapter.DataObjectBaseType;
			Type dataObjectType = Assembly.GetAssembly(dataObjectBaseType).GetTypes().Where(dataObjectBaseType.IsAssignableFrom).FirstOrDefault(x => x.Name.Equals(dataObjectName, StringComparison.OrdinalIgnoreCase));

			if (dataObjectType == null)
				throw new ArgumentOutOfRangeException("dataObjectName", dataObjectName, Resources.EphorteContext_Query_The_type_cannot_be_found);

			Type dataObjectQueryType = typeof(DataObjectQuery<>).MakeGenericType(dataObjectType);

			return (IQueryable)Activator.CreateInstance(dataObjectQueryType, StateManager.QueryProvider);
		}

		/// <summary>
		///     Adds the specified new data object.
		/// </summary>
		/// <param name="newDataObject">The new data object.</param>
		public void Add(object newDataObject)
		{
			StateManager.Add(newDataObject);
		}

		/// <summary>
		///     Removes the specified new data object.
		/// </summary>
		/// <param name="dataObject">The new data object.</param>
		public void Remove(object dataObject)
		{
			StateManager.Remove(dataObject);
		}

		/// <summary>
		///     Attaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		public void Attach(object dataObject)
		{
			StateManager.Attach(dataObject);
		}

		/// <summary>
		///     Detaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		public void Detach(object dataObject)
		{
			StateManager.Detach(dataObject);
		}

		/// <summary>
		///     Creates the type matching the specified <paramref name="dataObjectName" />.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <returns></returns>
		public object Create(string dataObjectName)
		{
			return _objectModelAdapter.Create(dataObjectName);
		}

		/// <summary>
		///     Raises the <see cref="SavedChanges" /> event.
		/// </summary>
		/// <param name="e">
		///     The <see cref="Gecko.NCore.Client.SavedChangesEventArgs" /> instance containing the event data.
		/// </param>
		protected virtual void OnSavedChanges(SavedChangesEventArgs e)
		{
			EventHandler<SavedChangesEventArgs> handler = SavedChanges;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		/// <summary>
		///     Raises the <see cref="SavingChanges" /> event.
		/// </summary>
		/// <param name="e">
		///     The <see cref="Gecko.NCore.Client.SavingChangesEventArgs" /> instance containing the event data.
		/// </param>
		protected void OnSavingChanges(SavingChangesEventArgs e)
		{
			EventHandler<SavingChangesEventArgs> handler = SavingChanges;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		/// <summary>
		///     Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing">
		///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public event EventHandler<SavedChangesEventArgs> SavedChanges;
		public event EventHandler<SavingChangesEventArgs> SavingChanges;
	}
}
