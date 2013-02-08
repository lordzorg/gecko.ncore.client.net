using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts.Claim.V1;

namespace Gecko.NCore.Host.Services.Claim.V1
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/claim/v1")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ClaimService : IClaimService
	{
		public IEnumerable<DataObjectClaims> GetDataObjectClaims(EphorteIdentity identity, IEnumerable<Identifier> identifiers)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<EphorteClaim> GetUserClaims(EphorteIdentity identity, int userId)
		{
			throw new NotImplementedException();
		}
	}
}