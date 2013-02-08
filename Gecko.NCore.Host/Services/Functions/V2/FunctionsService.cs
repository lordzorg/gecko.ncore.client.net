using System.Collections.Generic;
using System.ServiceModel;
using Ephorte.ServiceModel.Contracts.Functions.V2;

namespace Gecko.NCore.Host.Services.Functions.V2
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/functions/v2")]
    public class FunctionsService : IFunctionsService
    {
        public IEnumerable<FunctionDescriptor> QueryFunctionDescriptors(EphorteIdentity identity)
        {
            throw new System.NotImplementedException();
        }

        public FunctionResult ExecuteFunction(EphorteIdentity identity, string name, IEnumerable<object> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}