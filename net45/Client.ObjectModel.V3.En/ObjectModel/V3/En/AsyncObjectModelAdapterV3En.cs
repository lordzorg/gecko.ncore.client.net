using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	public class AsyncObjectModelAdapterV3En: ObjectModelAdapterV3En, IAsyncObjectModelAdapter
	{
		public AsyncObjectModelAdapterV3En(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(contextIdentity, settings)
		{
		}

	    public async Task<IEnumerable<object>> QueryAsync(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
	    {
            var filteredQueryArguments = new FilteredQueryArguments
            {
                DataObjectName = dataObjectName,
                FilterExpression = filterExpression,
                SortExpression = sortExpression,
                RelatedObjects = relatedObjects.ToArray(),
                SkipCount = skipCount,
                TakeCount = takeCount,
                ReturnTotalCount = false
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.FilteredQueryAsync(CreateEphorteIdentity(), filteredQueryArguments);
                return queryResult.DataObjects;
            }
        }

	    public async Task<int> QueryCountAsync(string dataObjectName, string filterExpression, string sortExpression)
	    {
            var filteredQueryArguments = new FilteredQueryArguments
            {
                DataObjectName = dataObjectName,
                FilterExpression = filterExpression,
                SortExpression = sortExpression,
                RelatedObjects = Enumerable.Empty<string>().ToArray(),
                TakeCount = 0,
                SkipCount = 0,
                ReturnTotalCount = true
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.FilteredQueryAsync(CreateEphorteIdentity(), filteredQueryArguments);
                return queryResult.TotalCount ?? 0;
            }
        }

	    public async Task<IEnumerable<object>> StoredQueryAsync(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
	    {
            var predefinedQueryArguments = new PredefinedQueryArguments
            {
                DataObjectName = dataObjectName,
                PredefinedSeachId = queryId,
                SortExpression = sortExpression,
                RelatedObjects = relatedObjects.ToArray(),
                SkipCount = skipCount,
                TakeCount = takeCount,
                ReturnTotalCount = false
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.PredefinedQueryAsync(CreateEphorteIdentity(), predefinedQueryArguments);
                return queryResult.DataObjects;
            }
        }

	    public async Task<int> StoredQueryCountAsync(string dataObjectName, int queryId, string sortExpression)
	    {
            var predefinedQueryArguments = new PredefinedQueryArguments
            {
                DataObjectName = dataObjectName,
                PredefinedSeachId = queryId,
                SortExpression = sortExpression,
                RelatedObjects = Enumerable.Empty<string>().ToArray(),
                TakeCount = 0,
                SkipCount = 0,
                ReturnTotalCount = true
            };

            using (var objectModelService = CreateServiceClient())
            {
                var queryResult = await objectModelService.PredefinedQueryAsync(CreateEphorteIdentity(), predefinedQueryArguments);
                return queryResult.TotalCount ?? 0;
            }
        }

	    public async System.Threading.Tasks.Task DeleteAsync(object dataObject)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                await objectModelService.DeleteAsync(CreateEphorteIdentity(), (DataObject)dataObject);
            }
        }

	    public async Task<IEnumerable<object>> BatchUpdateAsync(IEnumerable<object> modifiedDataObjects)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                return await objectModelService.UpdateAsync(CreateEphorteIdentity(), modifiedDataObjects.Cast<DataObject>().ToArray());
            }
        }

	    public async Task<IEnumerable<object>> BatchInsertAsync(IEnumerable<object> newDataObjects)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                return await objectModelService.InsertAsync(CreateEphorteIdentity(), newDataObjects.Cast<DataObject>().ToArray());
            }
        }

	    public async Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects)
	    {
            var primaryKeysCollection = new PrimaryKeyCollection();
            foreach (var primaryKey in primaryKeys)
                primaryKeysCollection.Add(primaryKey.Key, primaryKey.Value);

            var fetchArguments = new FetchArguments
            {
                DataObjectName = dataObjectName,
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

	    public async Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
	    {
            using (var objectModelService = CreateServiceClient())
            {
                int id;
                if (!primaryKeys.ContainsKey("Id") || !int.TryParse(primaryKeys["Id"], out id))
                {
                    id = 0;
                }

                var customFieldDescriptors = await objectModelService.GetCustomFieldDescriptorsAsync(CreateEphorteIdentity(), dataObjectName, category, id);
                return customFieldDescriptors.Cast<ICustomFieldDescriptor>().ToList();
            }
        }

	    public override Type DataObjectBaseType
		{
			get { return typeof (DataObject); }
		}

	}
}
