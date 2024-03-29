﻿using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts;
using Ephorte.ServiceModel.Contracts.Functions.V1;


namespace Gecko.NCore.Host.Services.Functions.V1
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FunctionsService : IFunctions
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