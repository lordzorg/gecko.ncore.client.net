using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gecko.NCore.Client.ObjectModel.V2
{
	public class ObjectModelAdapterV2: ObjectModelAdapterBase<ObjectModelServiceClient>
	{
		private readonly EphorteContextIdentity _contextIdentity;

		public ObjectModelAdapterV2(EphorteContextIdentity contextIdentity, ClientSettings settings)
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
				var queryResult = objectModelService.Query(CreateEphorteIdentity(), filteredQueryArguments);
				return queryResult.Result;
			}
		}

		public override int QueryCount(string dataObjectName, string filterExpression, string sortExpression)
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
				var queryResult = objectModelService.Query(CreateEphorteIdentity(), filteredQueryArguments);
				return (int)(queryResult.TotalCount ?? 0);
			}
		}

		public override IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
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
				var queryResult = objectModelService.Query(CreateEphorteIdentity(), filteredQueryArguments);
				return queryResult.Result;
			}
		}

		public override int StoredQueryCount(string dataObjectName, int queryId, string sortExpression)
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
				var queryResult = objectModelService.Query(CreateEphorteIdentity(), filteredQueryArguments);
				return (int)(queryResult.TotalCount ?? 0);
			}
		}

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
				objectModelService.Delete(CreateEphorteIdentity(), new TypedDeleteArguments {DataObject = (DataObject) dataObject});
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
				var result = (BatchUpdateResult)objectModelService.Update(CreateEphorteIdentity(), new BatchUpdateArguments { ModifiedObjects = modifiedDataObjects.Cast<DataObject>().ToArray() });
				return result.UpdatedObjects;
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
				var result = (BatchInsertResult)objectModelService.Insert(CreateEphorteIdentity(), new BatchInsertArguments { NewObjects = newDataObjects.Cast<DataObject>().ToArray() });
				return result.InsertedObjects;
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
										 DataObjectType = dataObjectName, 
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
			throw new NotSupportedException();
		}

	    public override Type DataObjectBaseType
		{
			get { return typeof (DataObject); }
		}
	}
}
