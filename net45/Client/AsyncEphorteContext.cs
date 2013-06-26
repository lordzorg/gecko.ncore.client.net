using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
	/// Data access context that provide asynchronous access to the ePhorte© Integration Services.
	/// </summary>
	public partial class EphorteContext
	{
		private readonly AsyncFunctionManager _asyncFunctionManager;
		private readonly AsyncDocumentManager _asyncDocumentManager;
		private readonly AsyncMetadataManager _asyncMetadataManager;
		private readonly IAsyncObjectModelAdapter _asyncObjectModelAdapter;

		public EphorteContext(
			IAsyncObjectModelAdapter asyncObjectModelAdapter = null, 
			IAsyncFunctionsAdapter asyncfunctionsAdapter = null,
			IAsyncDocumentsAdapter asyncDocumentsAdapter = null,
			IAsyncMetadataAdapter asyncMetadataAdapter = null, 
			NcoreVersion ncoreVersion = null
			) : this(CreateAsyncStateManager, asyncObjectModelAdapter, asyncfunctionsAdapter, asyncDocumentsAdapter, asyncMetadataAdapter, ncoreVersion)
		{
			if (asyncObjectModelAdapter != null)
				_asyncObjectModelAdapter = asyncObjectModelAdapter;

			if (asyncfunctionsAdapter != null)
				_asyncFunctionManager = new AsyncFunctionManager(asyncfunctionsAdapter);

			if (asyncDocumentsAdapter != null)
				_asyncDocumentManager = new AsyncDocumentManager(asyncDocumentsAdapter);

			if (asyncMetadataAdapter != null)
				_asyncMetadataManager = new AsyncMetadataManager(asyncMetadataAdapter);
		}

		private static StateManager CreateAsyncStateManager(IObjectModelAdapter objectModelAdapter, NcoreVersion ncoreVersion)
		{
			return objectModelAdapter == null 
				? null 
				: new StateManager(@this => new AsyncDataObjectQueryProvider(@this, (IAsyncObjectModelAdapter) objectModelAdapter, ncoreVersion));
		}

		public IAsyncFunctionManager FunctionsAsync
		{
			get
			{
				if (_asyncFunctionManager == null)
					throw new NotSupportedException(Resources.EphorteContext_Functions_Function_access_functionality_requires_the_IFunctionsAdapter_to_be_provided);

				return _asyncFunctionManager;
			}
		}

		public IAsyncDocumentManager DocumentsAsync
		{
			get
			{
				if (_asyncDocumentManager == null)
					throw new NotSupportedException(Resources.EphorteContext_Documents_Document_access_functionality_requires_the_IDocumentsAdapter_to_be_provided);
				return _asyncDocumentManager;
			}
		}

		public IAsyncMetadataManager MetadataAsync
		{
			get
			{
				if (_asyncMetadataManager == null)
					throw new NotSupportedException(Resources.EphorteContext_Metadata_Metadata_access_functionality_requires_the_IMetadataAdapter_to_be_provided);

				return _asyncMetadataManager;
			}
		}

		public async Task<IDataObjectAccess> InitializeAsync(object dataObject)
		{
			var dataObjectAccess = await _asyncObjectModelAdapter.InitializeAsync(dataObject);
			RefreshDataObject(dataObject, dataObjectAccess.DataObject);
			return dataObjectAccess;
		}

		public async Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> predicate, params string[] relatedObjects)
		{
			var dataObjectAccess = await _asyncObjectModelAdapter.FindAsync(dataObjectName, predicate, relatedObjects);
			_stateManager.WeakAttach(dataObjectAccess.DataObject);
			return dataObjectAccess;
		}

		public async Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorsAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
		{
			return await _asyncObjectModelAdapter.GetCustomFieldDescriptorAsync(dataObjectName, primaryKeys, category);
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
				await _asyncObjectModelAdapter.DeleteAsync(stateEntry.DataObject);
				_stateManager.Detach(stateEntry.DataObject);
			}
		}

		private async Task UpdateDataObjectsAsync(ICollection<StateEntry> stateEntries)
		{
			var updatedDataObjects = await _asyncObjectModelAdapter.BatchUpdateAsync(stateEntries.Select(x => x.DataObject));

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
			var insertedDataObjects = await _asyncObjectModelAdapter.BatchInsertAsync(stateEntries.Select(x => x.DataObject));
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
	}
}
