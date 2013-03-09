using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if USE_DTOs
using Ephorte.ServiceModel.Contracts.ObjectModel.V3.En;
using Gecko.NCore.Client;
using Gecko.NCore.Client.ObjectModel;

namespace Ephorte.ServiceModel.Client.ObjectModel.V3.En
#else
namespace Gecko.NCore.Client.ObjectModel.V3.En
#endif
{
	/// <summary>
	/// 
	/// </summary>
	public class AsyncObjectModelAdapterV3En: ObjectModelAdapterV3En, IAsyncObjectModelAdapter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contextIdentity"></param>
		/// <param name="settings"></param>
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
#if USE_DTOs
                PredefinedSearchId = queryId,
#else
                PredefinedSeachId = queryId,
#endif
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
#if USE_DTOs
                PredefinedSearchId = queryId,
#else
                PredefinedSeachId = queryId,
#endif
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

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="dataObject"></param>
	    /// <returns></returns>
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
            var fetchArguments = GetFetchArguments(dataObjectName, primaryKeys, relatedObjects);

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
                return GetCustomFieldDescriptors(customFieldDescriptors);
            }
        }

	    public override Type DataObjectBaseType
		{
			get { return typeof (DataObject); }
		}

	}
}
