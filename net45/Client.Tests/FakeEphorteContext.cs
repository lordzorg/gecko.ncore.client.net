using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.ObjectModel.V2;

namespace Gecko.NCore.Client.Tests
{
	class FakeEphorteContext : EphorteContext
	{
		public FakeEphorteContext() : this(new FakeObjectModelAdapter()) { }
		
		private readonly FakeObjectModelAdapter _objectModelAdapter;
		private FakeEphorteContext(FakeObjectModelAdapter objectModelAdapter) : base(objectModelAdapter, null, null,null)
		{
			_objectModelAdapter = objectModelAdapter;
		}

		public IList<QueryInfo> QueryArguments { get { return _objectModelAdapter.Queries; } }

		public Queue<IEnumerable<DataObject>> Results { get { return _objectModelAdapter.Results; } }

		public class QueryInfo
		{
			public QueryInfo(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
			{
				DataObjectName = dataObjectName;
				FilterExpression = filterExpression;
				SortExpression = sortExpression;
				RelatedObjects = relatedObjects;
				TakeCount = takeCount;
				SkipCount = skipCount;
			}

			public string DataObjectName { get; private set; }
			public string FilterExpression { get; private set; }
			private string _sortExpression;
			public string SortExpression
			{
				get { return string.IsNullOrEmpty(_sortExpression) ? null : _sortExpression; }
				private set { _sortExpression = value; }
			}

			public IEnumerable<string> RelatedObjects { get; private set; }
			public int? TakeCount { get; private set; }
			public int? SkipCount { get; private set; }
		}

		class FakeObjectModelAdapter : IObjectModelAdapter
		{
			private readonly Queue<IEnumerable<DataObject>> _results = new Queue<IEnumerable<DataObject>>();
			public Queue<IEnumerable<DataObject>> Results { get { return _results; } }

			public readonly List<QueryInfo> Queries = new List<QueryInfo>();
			public IEnumerable<object> Query(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
			{
				Queries.Add(new QueryInfo(dataObjectName, filterExpression, sortExpression, relatedObjects, takeCount, skipCount));
				return Results.Count != 0 ? Results.Dequeue() : new DataObject[] { };
			}

			public int QueryCount(string dataObjectName, string filterExpression, string sortExpression)
			{
				return Results.Count != 0 ? Results.Dequeue().Count() : 0;
			}

			public IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount)
			{
				throw new NotImplementedException();
			}

			public int StoredQueryCount(string dataObjectName, int queryId, string sortExpression)
			{
				throw new NotImplementedException();
			}

			public object Create(string dataObjectName)
			{
				throw new NotImplementedException();
			}

			public void Delete(object dataObject)
			{
				throw new NotImplementedException();
			}

			public IEnumerable<object> BatchUpdate(IEnumerable<object> modifiedDataObjects)
			{
				throw new NotImplementedException();
			}

			public IEnumerable<object> BatchInsert(IEnumerable<object> newDataObjects)
			{
				throw new NotImplementedException();
			}

			public IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects)
			{
				throw new NotImplementedException();
			}

			public IDataObjectAccess Initialize(object dataObject)
			{
				throw new NotImplementedException();
			}

			public Type DataObjectBaseType
		    {
                get { return typeof (DataObject); }
		    }

			public ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptor(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
			{
				throw new NotImplementedException();
			}

		    public Task<IEnumerable<object>> QueryAsync(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects,
		                           int? takeCount, int? skipCount)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<int> QueryCountAsync(string dataObjectName, string filterExpression, string sortExpression)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<IEnumerable<object>> StoredQueryAsync(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects,
		                                 int? takeCount, int? skipCount)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<int> StoredQueryCountAsync(string dataObjectName, int queryId, string sortExpression)
		    {
		        throw new NotImplementedException();
		    }

		    public Task DeleteAsync(object dataObject)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<IEnumerable<object>> BatchUpdateAsync(IEnumerable<object> modifiedDataObjects)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<IEnumerable<object>> BatchInsertAsync(IEnumerable<object> newDataObjects)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<IDataObjectAccess> FindAsync(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<IDataObjectAccess> InitializeAsync(object dataObject)
		    {
		        throw new NotImplementedException();
		    }

		    public Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorAsync(string dataObjectName, IDictionary<string, string> primaryKeys, string category)
		    {
		        throw new NotImplementedException();
		    }
		}
	}
}