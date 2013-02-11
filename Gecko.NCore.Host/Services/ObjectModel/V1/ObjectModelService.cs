using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Ephorte.ServiceModel.Contracts;
using Ephorte.ServiceModel.Contracts.ObjectModel.V1;


namespace Gecko.NCore.Host.Services.ObjectModel.V1
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/objectmodel/v1")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ObjectModelService : IObjectModelService
	{
		public IEnumerable<DataObject> ExecuteSelect(EphorteIdentity identity, string query)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSelectCount(EphorteIdentity identity, string query)
		{
			throw new NotImplementedException();
		}

		public DataObject ExecuteUpdate(EphorteIdentity identity, DataObject originalObject, DataObject modifiedObject)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DataObject> ExecuteBatchUpdate(EphorteIdentity identity, IEnumerable<ModifiedDataObject> modifiedObjects)
		{
			throw new NotImplementedException();
		}

		public DataObject ExecuteInsert(EphorteIdentity identity, DataObject modifiedObject)
		{
			throw new NotImplementedException();
		}

		public void ExecuteDelete(EphorteIdentity identity, DataObject dataObject)
		{
			throw new NotImplementedException();
		}

		public DataObject ApplyDefaults(EphorteIdentity identity, DataObject dataObject)
		{
			throw new NotImplementedException();
		}
	}
}