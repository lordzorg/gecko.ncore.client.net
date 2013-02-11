using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts.ObjectModel.V3.No;

namespace Gecko.NCore.Host.Services.ObjectModel.V3.No
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/objectmodel/v3/no")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ObjectModelService : IObjectModelService
	{
		public QueryResult FilteredQuery(EphorteIdentity identity, FilteredQueryArguments arguments)
		{
			throw new System.NotImplementedException();
		}

		public QueryResult PredefinedQuery(EphorteIdentity identity, PredefinedQueryArguments arguments)
		{
			throw new System.NotImplementedException();
		}

		public FetchResult Fetch(EphorteIdentity identity, FetchArguments arguments)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<DataObject> Update(EphorteIdentity identity, IEnumerable<DataObject> dataObjects)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<DataObject> Insert(EphorteIdentity identity, IEnumerable<DataObject> dataObjects)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(EphorteIdentity identity, DataObject dataObject)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteById(EphorteIdentity identity, string dataObjectName, PrimaryKeyCollection primaryKeys)
		{
			throw new System.NotImplementedException();
		}

		public InitializeResult Initialize(EphorteIdentity identity, InitializeArguments arguments)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<CustomFieldDescriptor> GetCustomFieldDescriptors(EphorteIdentity identity, string dataObjectName, string category, int id)
		{
			throw new System.NotImplementedException();
		}

		public ChangeSetResult Submit(EphorteIdentity identity, ChangeSet changeSet)
		{
			throw new System.NotImplementedException();
		}
	}
}