using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts.ChangeTracking.V1;

namespace Gecko.NCore.Host.Services.ChangeTracking.V1
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/changetracking/v1")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ChangeTrackingService : IChangeTrackingService
	{
		public IEnumerable<DataObjectChange> GetChangesForCaseDataObjects(EphorteIdentity identity, IEnumerable<int> caseIds, DateTime after)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DataObjectChange> GetChangesForRecordDataObjects(EphorteIdentity identity, IEnumerable<int> recordIds, DateTime after)
		{
			throw new NotImplementedException();
		}
	}
}