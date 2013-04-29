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
    public class AsyncEphorteContext : EphorteContext, IAsyncEphorteContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EphorteContext"/> class.
        /// </summary>
        /// <param name="objectModelAdapter">The object model adapter.</param>
        /// <param name="functionsAdapter">The function service adapter.</param>
        /// <param name="documentsAdapter">The documents adapter.</param>
        /// <param name="metadataAdapter">The metadata adapter.</param>
        public AsyncEphorteContext(IAsyncObjectModelAdapter objectModelAdapter, IAsyncFunctionsAdapter functionsAdapter, IAsyncDocumentsAdapter documentsAdapter, IAsyncMetadataAdapter metadataAdapter)
        {
            var stateManager = new StateManager(() => QueryProvider);
            var queryProvider = new AsyncDataObjectQueryProvider(stateManager, objectModelAdapter);
            var functionManager = new AsyncFunctionManager(functionsAdapter);
            var documentManager = new AsyncDocumentManager(documentsAdapter);
            var metadataManager = new AsyncMetadataManager(metadataAdapter);

            Init(objectModelAdapter, functionManager, documentManager, metadataManager, stateManager, queryProvider);
        }

        /// <summary>
        /// Gets the <see cref="FunctionManager"/>.
        /// </summary>
        /// <value>The <see cref="FunctionManager"/>.</value>
        public new IAsyncFunctionManager Functions
        {
            get { return (IAsyncFunctionManager)base.Functions;  }
        }

        /// <summary>
        /// Gets the documents.
        /// </summary>
        /// <value>The documents.</value>
        public new IAsyncDocumentManager Documents
        {
            get { return (IAsyncDocumentManager)base.Documents; }
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public new IAsyncMetadataManager Metadata
        {
            get { return (IAsyncMetadataManager)base.Metadata; }
        }

        protected new IAsyncObjectModelAdapter ObjectModelAdapter
        {
            get { return (IAsyncObjectModelAdapter)base.ObjectModelAdapter; }
        }

        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <returns>Task{IDataObjectAccess}.</returns>
        public async Task<IDataObjectAccess> InitializeAsync(object dataObject)
        {
            var dataObjectAccess = await ObjectModelAdapter.InitializeAsync(dataObject);
            RefreshDataObject(dataObject, dataObjectAccess.DataObject);
            return dataObjectAccess;
        }

        /// <summary>
        /// Finds the async.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="relatedObjects">The related objects.</param>
        /// <returns>Task{IDataObjectAccess}.</returns>
        public async Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> predicate, params string[] relatedObjects)
        {
            var dataObjectAccess = await ObjectModelAdapter.FindAsync(dataObjectName, predicate, relatedObjects);
            StateManager.WeakAttach(dataObjectAccess.DataObject);
            return dataObjectAccess;
        }

        /// <summary>
        /// Gets the custom field descriptors async.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="primaryKeys">The primary keys.</param>
        /// <param name="category">The category.</param>
        /// <returns>Task{ICollection{ICustomFieldDescriptor}}.</returns>
        public async Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorsAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
        {
            return await ObjectModelAdapter.GetCustomFieldDescriptorAsync(dataObjectName, primaryKeys, category);
        }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task SaveChangesAsync()
        {
            OnSavingChanges(new SavingChangesEventArgs(StateManager));
            foreach (var stateEntryGroup in StateManager.Entries.GroupBy(x => x.State).ToList())
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
            OnSavedChanges(new SavedChangesEventArgs(StateManager));
        }

        private async Task DeleteDataObjectsAsync(IEnumerable<StateEntry> stateEntries)
        {
            foreach (var stateEntry in stateEntries)
            {
                await ObjectModelAdapter.DeleteAsync(stateEntry.DataObject);
                StateManager.Detach(stateEntry.DataObject);
            }
        }

        private async Task UpdateDataObjectsAsync(ICollection<StateEntry> stateEntries)
        {
            var updatedDataObjects = await ObjectModelAdapter.BatchUpdateAsync(stateEntries.Select(x => x.DataObject));

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
            var insertedDataObjects = await ObjectModelAdapter.BatchInsertAsync(stateEntries.Select(x => x.DataObject));
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
                if (ObjectModelAdapter.DataObjectBaseType.IsAssignableFrom(property.PropertyType))
                    continue;

                var elementType = TypeSystem.ResolveElementType(property.PropertyType);
                if (elementType != null && ObjectModelAdapter.DataObjectBaseType.IsAssignableFrom(elementType))
                    continue;

                property.SetValue(target, property.GetValue(source, null), null);
            }
        }
    }
}
