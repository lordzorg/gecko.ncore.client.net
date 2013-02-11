using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Ephorte.ServiceModel.Contracts.ObjectModel.V2;
using Ephorte.ServiceModel.Contracts.ObjectModel.V2.DataObjects;

namespace Gecko.NCore.Host.Services.ObjectModel.V2
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/objectmodel/v2")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ObjectModelService : IObjectModelService
	{
		public QueryResult Query(EphorteIdentity identity, QueryArguments arguments)
		{
			throw new NotImplementedException();
		}

		public FetchResult Fetch(EphorteIdentity identity, FetchArguments arguments)
		{
			throw new NotImplementedException();
		}

		public UpdateResult Update(EphorteIdentity identity, UpdateArguments arguments)
		{
			throw new NotImplementedException();
		}

		public InsertResult Insert(EphorteIdentity identity, InsertArguments arguments)
		{
			throw new NotImplementedException();
		}

		public void Delete(EphorteIdentity identity, DeleteArguments arguments)
		{
			throw new NotImplementedException();
		}

		public InitializeResult Initialize(EphorteIdentity identity, InitializeArguments arguments)
		{
			throw new NotImplementedException();
		}

		public void CheckinDocument(EphorteIdentity ephorteIdentity, int documentId, int version, string variant, string identifier)
		{
			throw new NotImplementedException();
		}

		public void CheckoutDocument(EphorteIdentity identity, int documentId, int version, string variant)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckoutDocument(EphorteIdentity identity, int journalpostId, int documentId, int version)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<FunctionDescriptor> QueryFunctionDescriptors(EphorteIdentity identity)
		{
			throw new NotImplementedException();
		}

		public FunctionResult ExecuteFunction(EphorteIdentity identity, string name, IEnumerable<string> parameters)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CustomFieldDescriptor> GetCustomFieldDescriptors(EphorteIdentity identity, string dataObjectType, string category, int id)
		{
			throw new NotImplementedException();
		}
	}
}