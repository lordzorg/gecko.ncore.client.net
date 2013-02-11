using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
    /// <summary>
    /// ObjectModelAdapterV3No
    /// </summary>
	public class ObjectModelAdapterV3No: ObjectModelAdapterBase<ObjectModelServiceClient>
	{
		private readonly EphorteContextIdentity _contextIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectModelAdapterV3No" /> class.
        /// </summary>
        /// <param name="contextIdentity">The context identity.</param>
        /// <param name="settings">The settings.</param>
		public ObjectModelAdapterV3No(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(settings)
		{
			_contextIdentity = contextIdentity;
		}

        /// <summary>
        /// Creates the ephorte identity.
        /// </summary>
        /// <returns>EphorteIdentity.</returns>
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

        /// <summary>
        /// Queries the specified data object name.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="relatedObjects">The related objects.</param>
        /// <param name="takeCount">The take count.</param>
        /// <param name="skipCount">The skip count.</param>
        /// <returns>IEnumerable{System.Object}.</returns>
		public override IEnumerable<object> Query(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
		{
			var relatedObjectList = (relatedObjects ?? Enumerable.Empty<string>()).ToArray();
			Trace("ObjectModelAdapter.V3.No", "Query DataObjectName: {0}; Filter: {1}; Sort: {2}; Related: {3}; Take: {4}; Skip: {5}", dataObjectName, filterExpression, sortExpression, string.Join(", ", relatedObjectList.ToArray()), takeCount.HasValue ? takeCount.Value.ToString(CultureInfo.InvariantCulture) : "@", skipCount.HasValue ? skipCount.Value.ToString(CultureInfo.InvariantCulture) : "@");
			var filteredQueryArguments = new FilteredQueryArguments
											 {
												 DataObjectName = dataObjectName,
												 FilterExpression = filterExpression,
												 SortExpression = sortExpression,
												 RelatedObjects = relatedObjectList,
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

        /// <summary>
        /// Queries the count.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns>System.Int32.</returns>
		public override int QueryCount(string dataObjectName, string filterExpression, string sortExpression)
		{
			Trace("ObjectModelAdapter.V3.No", "QueryCount DataObjectName: {0}; Filter: {1}; Sort: {2}", dataObjectName, filterExpression, sortExpression);
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

        /// <summary>
        /// Storeds the query.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="queryId">The query id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="relatedObjects">The related objects.</param>
        /// <param name="takeCount">The take count.</param>
        /// <param name="skipCount">The skip count.</param>
        /// <returns>IEnumerable{System.Object}.</returns>
		public override IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
		{
			var relatedObjectList = (relatedObjects ?? Enumerable.Empty<string>()).ToArray();
			Trace("ObjectModelAdapter.V3.No", "Stored Query DataObjectName: {0}; Query Id: {1}; Sort: {2}; Related: {3}; Take: {4}; Skip: {5}", dataObjectName, queryId, sortExpression, string.Join(", ", relatedObjectList.ToArray()), takeCount.HasValue ? takeCount.Value.ToString(CultureInfo.InvariantCulture) : "@", skipCount.HasValue ? skipCount.Value.ToString(CultureInfo.InvariantCulture) : "@");
			var predefinedQueryArguments = new PredefinedQueryArguments
			                               	{
				DataObjectName = dataObjectName,
				PredefinedSeachId = queryId,
				SortExpression = sortExpression,
				RelatedObjects = relatedObjectList,
				SkipCount = skipCount,
				TakeCount = takeCount,
				ReturnTotalCount = false
			};

			using (var objectModelService = CreateServiceClient())
			{
				var queryResult = objectModelService.PredefinedQuery(CreateEphorteIdentity(), predefinedQueryArguments);
				foreach (DataObject o in queryResult.DataObjects)
					yield return o;
			}
		}

        /// <summary>
        /// Storeds the query count.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="queryId">The query id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns>System.Int32.</returns>
		public override int StoredQueryCount(string dataObjectName, int queryId, string sortExpression)
		{
			Trace("ObjectModelAdapter.V3.No", "Stored Query Count DataObjectName: {0}; Query Id: {1}; Sort: {2}", dataObjectName, queryId, sortExpression);
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

		/// <summary>
		/// Finds the specified predicate.
		/// </summary>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="primaryKeys">The primary keys.</param>
		/// <param name="relatedObjects">The related objects.</param>
		/// <returns></returns>
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

        /// <summary>
        /// Gets the custom field descriptor.
        /// </summary>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="primaryKeys">The primary keys.</param>
        /// <param name="category">The category.</param>
        /// <returns>ICollection{ICustomFieldDescriptor}.</returns>
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

        /// <summary>
        /// Gets the type of the data object base.
        /// </summary>
        /// <value>The type of the data object base.</value>
	    public override Type DataObjectBaseType
		{
			get { return typeof (DataObject); }
		}

		[Conditional("DEBUG")]
		private static void Trace(string category, string format, params object[] args)
		{
			System.Diagnostics.Trace.WriteLine(string.Format(format, args), category);
		}
	}
}
