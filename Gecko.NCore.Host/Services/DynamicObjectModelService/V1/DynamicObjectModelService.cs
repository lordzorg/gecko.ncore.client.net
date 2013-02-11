using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Ephorte.ServiceModel.Contracts.DynamicObjectModel.V1;

namespace Gecko.NCore.Host.Services.DynamicObjectModelService.V1
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DynamicObjectModelService : IDynamicObjectModelService
	{
		public IEnumerable<TaskResult> ExecuteTask(EphorteIdentity identity, IEnumerable<Task> tasks)
		{
			throw new NotImplementedException();
		}
	}
}