using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gecko.NCore.Client.Properties;
using Gecko.NCore.Client.Documents;
using Gecko.NCore.Client.Functions;
using Gecko.NCore.Client.Metadata;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Querying;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client
{
    /// <summary>
    /// This class provides a context for access to the ePhorte© Integration Services.
    /// </summary>
    public class EphorteContext : IEphorteContext
    {
        private readonly IObjectModelAdapter _objectModelAdapter;
        private readonly IQueryProvider _queryProvider;
        private readonly IStateManager _stateManager;
        private readonly FunctionManager _functionManager;
        private readonly DocumentManager _documentManager;
        private readonly MetadataManager _metadataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EphorteContext"/> class.
        /// </summary>
        /// <param name="objectModelAdapter">The object model adapter.</param>
        /// <param name="functionsAdapter">The function service adapter.</param>
        /// <param name="documentsAdapter">The documents adapter.</param>
        /// <param name="metadataAdapter">The metadata adapter.</param>
        public EphorteContext(IObjectModelAdapter objectModelAdapter, IFunctionsAdapter functionsAdapter, IDocumentsAdapter documentsAdapter, IMetadataAdapter metadataAdapter)
        {
            _objectModelAdapter = objectModelAdapter;
            _stateManager = new StateManager(() => _queryProvider);
            _queryProvider = new DataObjectQueryProvider(_stateManager, _objectModelAdapter);
            _functionManager = new FunctionManager(functionsAdapter);
            _documentManager = new DocumentManager(documentsAdapter);
            _metadataManager = new MetadataManager(metadataAdapter);
        }

        /// <summary>
        /// Gets the <see cref="FunctionManager"/>.
        /// </summary>
        /// <value>The <see cref="FunctionManager"/>.</value>
        public IFunctionManager Functions
        {
            get { return _functionManager;  }
        }

        /// <summary>
        /// Gets the documents.
        /// </summary>
        /// <value>The documents.</value>
        public IDocumentManager Documents
        {
            get { return _documentManager; }
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public IMetadataManager Metadata
        {
            get { return _metadataManager; }
        }

        /// <summary>
        /// Creates a <see cref="IQueryable{TDataObject}">queryable</see> that be used to query the ePhorte© Integration Services.
        /// </summary>
        /// <typeparam name="TDataObject">The type of the data object.</typeparam>
        /// <returns>A <see cref="IQueryable{TDataObject}"/> instance.</returns>
        public virtual IQueryable<TDataObject> Query<TDataObject>() where TDataObject : class
        {
            return new DataObjectQuery<TDataObject>(_queryProvider);
        }

        /// <summary>
        /// Creates a <see cref="IQueryable">queryable</see> that be used to query the ePhorte© Integration Services.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <returns>
        /// A <see cref="IQueryable"/> instance.
        /// </returns>
        public IQueryable Query(string dataObjectName)
        {
            if (string.IsNullOrEmpty(dataObjectName))
                throw new ArgumentNullException("dataObjectName");

            var dataObjectBaseType = _objectModelAdapter.DataObjectBaseType;
            var dataObjectType = Assembly.GetAssembly(dataObjectBaseType).GetTypes().Where(dataObjectBaseType.IsAssignableFrom).FirstOrDefault(x => x.Name.Equals(dataObjectName, StringComparison.OrdinalIgnoreCase));

			if (dataObjectType == null)
				throw new ArgumentOutOfRangeException("dataObjectName", dataObjectName, Resources.EphorteContext_Query_The_type_cannot_be_found);

            var dataObjectQueryType = typeof (DataObjectQuery<>).MakeGenericType(dataObjectType);

            return (IQueryable) Activator.CreateInstance(dataObjectQueryType, _queryProvider);
        }

		/// <summary>
		/// Adds the specified new data object.
		/// </summary>
		/// <param name="newDataObject">The new data object.</param>
        public void Add(object newDataObject)
        {
            _stateManager.Add(newDataObject);
        }

		/// <summary>
		/// Removes the specified new data object.
		/// </summary>
		/// <param name="dataObject">The new data object.</param>
        public void Remove(object dataObject)
        {
            _stateManager.Remove(dataObject);
        }

		/// <summary>
		/// Attaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
        public void Attach(object dataObject)
        {
            _stateManager.Attach(dataObject);
        }

		/// <summary>
		/// Detaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
        public void Detach(object dataObject)
        {
            _stateManager.Detach(dataObject);
        }

        /// <summary>
        /// Creates the type matching the specified <paramref name="dataObjectName"/>.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <returns></returns>
        public object Create(string dataObjectName)
        {
            return _objectModelAdapter.Create(dataObjectName);
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
			_stateManager.WeakAttach(dataObjectAccess.DataObject);
			return dataObjectAccess;
		}

		public ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptors(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
		{
			return _objectModelAdapter.GetCustomFieldDescriptor(dataObjectName, primaryKeys, category);
		}

    	/// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
    	{
    		OnSavingChanges(new SavingChangesEventArgs(_stateManager));
            foreach (var stateEntryGroup in _stateManager.Entries.GroupBy(x => x.State).ToList())
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
    	    OnSavedChanges(new SavedChangesEventArgs(_stateManager));
    	}

        public event EventHandler<SavedChangesEventArgs> SavedChanges;

		/// <summary>
		/// Raises the <see cref="SavedChanges"/> event.
		/// </summary>
		/// <param name="e">The <see cref="Gecko.NCore.Client.SavedChangesEventArgs"/> instance containing the event data.</param>
    	protected virtual void OnSavedChanges(SavedChangesEventArgs e)
    	{
    		var handler = SavedChanges;
			if (handler != null)
			{
				handler(this, e);
			}
    	}

    	public event EventHandler<SavingChangesEventArgs> SavingChanges;

		/// <summary>
		/// Raises the <see cref="SavingChanges"/> event.
		/// </summary>
		/// <param name="e">The <see cref="Gecko.NCore.Client.SavingChangesEventArgs"/> instance containing the event data.</param>
    	protected virtual void OnSavingChanges(SavingChangesEventArgs e)
    	{
    		var handler = SavingChanges;
			if (handler != null)
			{
				handler(this, e);
			}
    	}

    	private void DeleteDataObjects(IEnumerable<StateEntry> stateEntries)
        {
            foreach (var stateEntry in stateEntries)
            {
                _objectModelAdapter.Delete(stateEntry.DataObject);
                _stateManager.Detach(stateEntry.DataObject);
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

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
