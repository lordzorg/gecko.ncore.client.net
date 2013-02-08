using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts.ePhorteWebInternal;

namespace Gecko.NCore.Host.Services.ePhorteWebInternal
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/EphorteWebInternal")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ePhorteWebInternalService : IEphorteWebInternalService
	{
		public DataSet Seek(EphorteIdentity identity, List<string> criteriaCollection, bool returnRowCount)
		{
			throw new NotImplementedException();
		}

		public DataSet PredefinedSeek(EphorteIdentity identity, int predefinedSearchId, string fieldList, bool returnRowCount)
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, EphColumnWcf> GetEphColumns(EphorteIdentity identity, string criteriaCollectionString, bool addNewRecord)
		{
			throw new NotImplementedException();
		}

		public EphPermissionWcf GetEphPermission(EphorteIdentity identity, string criteriaCollectionString, bool addNewRecord)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DataSet> GetLegalValues(EphorteIdentity identity, IEnumerable<LegalValuesParametersWcf> legalValuesParameters)
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, EphColumnWcf> UpdateEphObject(EphorteIdentity identity,
		                                  string criteriaCollectionString,
		                                  bool refreshRecord,
		                                  bool addNewRecord,
		                                  Dictionary<string, EphColumnWcf> data,
		                                  string lastRunSearch)
		{
			throw new NotImplementedException();
		}

		public void DeleteEphObject(EphorteIdentity identity, string criteriaCollectionString)
		{
			throw new NotImplementedException();
		}
	}
}