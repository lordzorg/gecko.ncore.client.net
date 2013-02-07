using System;
using System.Collections.Generic;

namespace Gecko.NCore.Client.ObjectModel
{
    public abstract class ObjectModelAdapterBase<TServiceClient>: ServiceAdapterBase<TServiceClient>, IObjectModelAdapter where TServiceClient : class, new()
    {
        protected ObjectModelAdapterBase(ClientSettings settings)
            : base(settings)
        {
        }

        public abstract Type DataObjectBaseType { get; }

        public abstract IEnumerable<object> Query(string dataObjectName, string filterExpression, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);
        public abstract int QueryCount(string dataObjectName, string filterExpression, string sortExpression);
        public abstract IEnumerable<object> StoredQuery(string dataObjectName, int queryId, string sortExpression, IEnumerable<string> relatedObjects, int? takeCount, int? skipCount);
        public abstract int StoredQueryCount(string dataObjectName, int queryId, string sortExpression);
        public abstract object Create(string dataObjectName);
        public abstract void Delete(object dataObject);
        public abstract IEnumerable<object> BatchUpdate(IEnumerable<object> modifiedDataObjects);
        public abstract IEnumerable<object> BatchInsert(IEnumerable<object> newDataObjects);
        public abstract IDataObjectAccess Find(string dataObjectName, IDictionary<string, string> primaryKeys, params string[] relatedObjects);
        public abstract IDataObjectAccess Initialize(object dataObject);
        public abstract ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptor(string dataObjectName, IDictionary<string, string> primaryKeys, string category);
    }
}
