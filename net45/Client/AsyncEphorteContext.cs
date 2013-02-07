using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AsyncEphorteContext : EphorteContext
    {
        private readonly IAsyncObjectModelAdapter _objectModelAdapter;
        private readonly IQueryProvider _queryProvider;
        private readonly IStateManager _stateManager;
        private readonly AsyncFunctionManager _functionManager;
        private readonly AsyncDocumentManager _documentManager;
        private readonly AsyncMetadataManager _metadataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EphorteContext"/> class.
        /// </summary>
        /// <param name="objectModelAdapter">The object model adapter.</param>
        /// <param name="functionsAdapter">The function service adapter.</param>
        /// <param name="documentsAdapter">The documents adapter.</param>
        /// <param name="metadataAdapter">The metadata adapter.</param>
        public AsyncEphorteContext(IAsyncObjectModelAdapter objectModelAdapter, IAsyncFunctionsAdapter functionsAdapter, IAsyncDocumentsAdapter documentsAdapter, IAsyncMetadataAdapter metadataAdapter)
            : base(objectModelAdapter, functionsAdapter, documentsAdapter, metadataAdapter)
        {
            _objectModelAdapter = objectModelAdapter;
            _stateManager = new StateManager(() => _queryProvider);
            _queryProvider = new AsyncDataObjectQueryProvider(_stateManager, _objectModelAdapter);
            _functionManager = new AsyncFunctionManager(functionsAdapter);
            _documentManager = new AsyncDocumentManager(documentsAdapter);
            _metadataManager = new AsyncMetadataManager(metadataAdapter);
        }

        /// <summary>
        /// Gets the <see cref="FunctionManager"/>.
        /// </summary>
        /// <value>The <see cref="FunctionManager"/>.</value>
        public new IAsyncFunctionManager Functions
        {
            get { return _functionManager;  }
        }

        /// <summary>
        /// Gets the documents.
        /// </summary>
        /// <value>The documents.</value>
        public new IAsyncDocumentManager Documents
        {
            get { return _documentManager; }
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public new IAsyncMetadataManager Metadata
        {
            get { return _metadataManager; }
        }

        public async Task<IDataObjectAccess> InitializeAsync(object dataObject)
        {
            var dataObjectAccess = await _objectModelAdapter.InitializeAsync(dataObject);
            RefreshDataObject(dataObject, dataObjectAccess.DataObject);
            return dataObjectAccess;
        }

        public async Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> predicate, params string[] relatedObjects)
        {
            var dataObjectAccess = await _objectModelAdapter.FindAsync(dataObjectName, predicate, relatedObjects);
            _stateManager.WeakAttach(dataObjectAccess.DataObject);
            return dataObjectAccess;
        }

        public async Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorsAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
        {
            return await _objectModelAdapter.GetCustomFieldDescriptorAsync(dataObjectName, primaryKeys, category);
        }

        public async Task SaveChangesAsync()
        {
            OnSavingChanges(new SavingChangesEventArgs(_stateManager));
            foreach (var stateEntryGroup in _stateManager.Entries.GroupBy(x => x.State).ToList())
            {
                var stateEntries = stateEntryGroup.ToList();
                switch (stateEntryGroup.Key)
                {
                    case DataObjectState.Added:
                        await InsertDataObjectsAsync(stateEntries);
                        break;
                    case DataObjectState.Modified:
                        await UpdateDataObjectsAsync(stateEntries);
                        break;
                    case DataObjectState.Removed:
                        await DeleteDataObjectsAsync(stateEntries);
                        break;
                }
            }
            OnSavedChanges(new SavedChangesEventArgs(_stateManager));
        }

        private async Task DeleteDataObjectsAsync(IEnumerable<StateEntry> stateEntries)
        {
            foreach (var stateEntry in stateEntries)
            {
                await _objectModelAdapter.DeleteAsync(stateEntry.DataObject);
                _stateManager.Detach(stateEntry.DataObject);
            }
        }

        private async Task UpdateDataObjectsAsync(ICollection<StateEntry> stateEntries)
        {
            var updatedDataObjects = await _objectModelAdapter.BatchUpdateAsync(stateEntries.Select(x => x.DataObject));

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

        private async Task InsertDataObjectsAsync(ICollection<StateEntry> stateEntries)
        {
            var insertedDataObjects = await _objectModelAdapter.BatchInsertAsync(stateEntries.Select(x => x.DataObject));
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
    }
}
