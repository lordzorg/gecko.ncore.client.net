using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.ObjectModel.V2
{
	public class AsyncObjectModelAdapterV2: ObjectModelAdapterV2, IAsyncObjectModelAdapter
	{
		public AsyncObjectModelAdapterV2(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(contextIdentity, settings)
		{
		}

        public async Task<IEnumerable<object>> QueryAsync(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
	    {
            var filteredQueryArguments = new GenericQueryArguments
            {
                DataObjectType = dataObjectName,
                WhereExpression = filterExpression,
                SortExpression = sortExpression,
                RelatedObjects = relatedObjects.ToArray(),
                SkipCount = skipCount,
                TakeCount = takeCount,
                ReturnTotalCount = false
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.QueryAsync(CreateEphorteIdentity(), filteredQueryArguments);
                return queryResult.Result;
            }
        }

	    public async Task<int> QueryCountAsync(string dataObjectName, string filterExpression, string sortExpression)
	    {
            var filteredQueryArguments = new GenericQueryArguments
            {
                DataObjectType = dataObjectName,
                WhereExpression = filterExpression,
                SortExpression = sortExpression,
                RelatedObjects = Enumerable.Empty<string>().ToArray(),
                TakeCount = 0,
                SkipCount = 0,
                ReturnTotalCount = true
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.QueryAsync(CreateEphorteIdentity(), filteredQueryArguments);
                return (int)(queryResult.TotalCount ?? 0);
            }
        }

	    public async Task<IEnumerable<object>> StoredQueryAsync(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
	    {
            var filteredQueryArguments = new PredefinedQueryArguments
            {
                DataObjectType = dataObjectName,
                PredefinedSoekId = queryId,
                SortExpression = sortExpression,
                RelatedObjects = relatedObjects.ToArray(),
                SkipCount = skipCount,
                TakeCount = takeCount,
                ReturnTotalCount = false
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.QueryAsync(CreateEphorteIdentity(), filteredQueryArguments);
                return queryResult.Result;
            }
        }

	    public async Task<int> StoredQueryCountAsync(string dataObjectName, int queryId, string sortExpression)
	    {
            var filteredQueryArguments = new PredefinedQueryArguments
            {
                DataObjectType = dataObjectName,
                PredefinedSoekId = queryId,
                SortExpression = sortExpression,
                RelatedObjects = Enumerable.Empty<string>().ToArray(),
                TakeCount = 0,
                SkipCount = 0,
                ReturnTotalCount = true
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.QueryAsync(CreateEphorteIdentity(), filteredQueryArguments);
                return (int)(queryResult.TotalCount ?? 0);
            }
        }

	    public async Task DeleteAsync(object dataObject)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                await objectModelService.DeleteAsync(CreateEphorteIdentity(), new TypedDeleteArguments { DataObject = (DataObject)dataObject });
            }
        }

	    public async Task<IEnumerable<object>> BatchUpdateAsync(IEnumerable<object> modifiedDataObjects)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                var result = (BatchUpdateResult)await objectModelService.UpdateAsync(CreateEphorteIdentity(), new BatchUpdateArguments { ModifiedObjects = modifiedDataObjects.Cast<DataObject>().ToArray() });
                return result.UpdatedObjects;
            }
        }

	    public async Task<IEnumerable<object>> BatchInsertAsync(IEnumerable<object> newDataObjects)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                var result = (BatchInsertResult)await objectModelService.InsertAsync(CreateEphorteIdentity(), new BatchInsertArguments { NewObjects = newDataObjects.Cast<DataObject>().ToArray() });
                return result.InsertedObjects;
            }
        }

	    public async Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects)
	    {
            var primaryKeysCollection = new PrimaryKeyCollection();
            foreach (var primaryKey in primaryKeys)
                primaryKeysCollection.Add(primaryKey.Key, primaryKey.Value);

            var fetchArguments = new FetchArguments
            {
                DataObjectType = dataObjectName,
                PrimaryKeys = primaryKeysCollection,
                RelatedObjects = relatedObjects.ToArray(),
                ReturnAccessRights = true,
                ReturnRequiredFields = true
            };

            using (var objectModelService = CreateServiceClient())
            {
                var result = await objectModelService.FetchAsync(CreateEphorteIdentity(), fetchArguments);
                return new DataObjectAccess(result.DataObject, result.RequiredFields, result.AccessRights, result.ObjectRights);
            }
        }

	    public async Task<IDataObjectAccess> InitializeAsync(object dataObject)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                var result = await objectModelService.InitializeAsync(CreateEphorteIdentity(), new InitializeArguments { DataObject = dataObject as DataObject, ReturnAccessRights = true, ReturnRequiredFields = true });
                return new DataObjectAccess(result.DataObject, result.RequiredFields, result.AccessRights, result.ObjectRights);
            }
        }

	    public Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
	    {
	        var taskCompletionSource = new TaskCompletionSource<ICollection<ICustomFieldDescriptor>>();
            taskCompletionSource.SetException(new NotSupportedException());
	        return taskCompletionSource.Task;
	    }
	}
}
