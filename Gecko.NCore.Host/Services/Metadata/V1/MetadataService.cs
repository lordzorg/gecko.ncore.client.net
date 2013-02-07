using System.ServiceModel;
using Ephorte.ServiceModel.Contracts;
using Ephorte.ServiceModel.Contracts.Metadata.V1;


namespace Gecko.NCore.Host.Services.Metadata.V1
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	public class MetadataService : IMetadataService
    {
        public MetadataDictionary GetMetadata(EphorteIdentity identity, MetadataIdentifier identifier)
        {
            throw new System.NotImplementedException();
        }
    }
}