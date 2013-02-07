using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	public class ObjectModelAdapterV3En: ObjectModelAdapterBase<ObjectModelServiceClient>
	{
		private readonly EphorteContextIdentity _contextIdentity;
		
        public ObjectModelAdapterV3En(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(settings)
		{
			_contextIdentity = contextIdentity;
		}

		protected EphorteIdentity CreateEphorteIdentity()
		{
			return new EphorteIdentity
					   {
						   UserName = _contextIdentity.Username, 
						   Password = _contextIdentity.Password,
						   Role = _contextIdentity.Role,
						   Database = _contextIdentity.Database,
						   ExternalSystemName = _contextIdentity.ExternalSystemName
					   };
		}

		public override IEnumerable<object> Query(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
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
				var queryResult = objectModelService.FilteredQuery(CreateEphorteIdentity(), filteredQueryArguments);
				return queryResult.DataObjects;
			}
		}

		public override int QueryCount(string dataObjectName, string filterExpression, string sortExpression)
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
				var queryResult = objectModelService.FilteredQuery(CreateEphorteIdentity(), filteredQueryArguments);
				return queryResult.TotalCount ?? 0;
			}
		}

		public override IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
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
				var queryResult = objectModelService.PredefinedQuery(CreateEphorteIdentity(), predefinedQueryArguments);
				return queryResult.DataObjects;
			}
		}

		public override int StoredQueryCount(string dataObjectName, int queryId, string sortExpression)
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
				var queryResult = objectModelService.PredefinedQuery(CreateEphorteIdentity(), predefinedQueryArguments);
				return queryResult.TotalCount ?? 0;
			}
		}
		
		/// <summary>
		/// Creates the specified data object name.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <returns></returns>
		public override object Create(string dataObjectName)
		{
			var dataObjectBaseType = typeof(DataObject);
			var dataObjectTypeQuery = from t in Assembly.GetAssembly(dataObjectBaseType).GetTypes()
									  where t.IsSubclassOf(dataObjectBaseType) && t.Name == dataObjectName
									  select t;

			return Activator.CreateInstance(dataObjectTypeQuery.First());
		}

		/// <summary>
		/// Deletes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		public override void Delete(object dataObject)
		{
			using (var objectModelService = CreateServiceClient())
			{
				objectModelService.Delete(CreateEphorteIdentity(), (DataObject) dataObject);
			}
		}

		/// <summary>
		/// Updates the <paramref name="modifiedDataObjects"/> as a batch operation.
		/// </summary>
		/// <param name="modifiedDataObjects">The modified data objects.</param>
		/// <returns></returns>
		public override IEnumerable<object> BatchUpdate(IEnumerable<object> modifiedDataObjects)
		{
			using (var objectModelService = CreateServiceClient())
			{
				return objectModelService.Update(CreateEphorteIdentity(), modifiedDataObjects.Cast<DataObject>().ToArray());
			}
		}

		/// <summary>
		/// Inserts the <paramref name="newDataObjects"/> as a batch operation.
		/// </summary>
		/// <param name="newDataObjects">The new data objects.</param>
		/// <returns></returns>
		public override IEnumerable<object> BatchInsert(IEnumerable<object> newDataObjects)
		{
			using (var objectModelService = CreateServiceClient())
			{
				return objectModelService.Insert(CreateEphorteIdentity(), newDataObjects.Cast<DataObject>().ToArray());
			}
		}

		/// <summary>
		/// Initializes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		/// <returns></returns>
		public override IDataObjectAccess Initialize(object dataObject)
		{
			using (var objectModelService = CreateServiceClient())
			{
				var result = objectModelService.Initialize(CreateEphorteIdentity(), new InitializeArguments { DataObject = dataObject as DataObject, ReturnAccessRights = true, ReturnRequiredFields = true });
				return new DataObjectAccess(result.DataObject, result.RequiredFields, result.AccessRights, result.ObjectRights);
			}
		}

		public override IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects)
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
				var result = objectModelService.Fetch(CreateEphorteIdentity(), fetchArguments);
				return new DataObjectAccess(result.DataObject, result.RequiredFields, result.AccessRights, result.ObjectRights);
			}
		}

		public override ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptor(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
		{
			using (var objectModelService = CreateServiceClient())
			{
				int id;
				if (!primaryKeys.ContainsKey("Id") || !int.TryParse(primaryKeys["Id"], out id))
				{
					id = 0;
				}

				var customFieldDescriptors = objectModelService.GetCustomFieldDescriptors(CreateEphorteIdentity(), dataObjectName, category, id);
				return customFieldDescriptors.Cast<ICustomFieldDescriptor>().ToList();
			}
		}

	    public override Type DataObjectBaseType
		{
			get { return typeof (DataObject); }
		}

	}
}
