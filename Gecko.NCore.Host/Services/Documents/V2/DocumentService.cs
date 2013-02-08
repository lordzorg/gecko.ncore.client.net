using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts;
using Ephorte.ServiceModel.Contracts.Documents.V2;

namespace Gecko.NCore.Host.Services.Documents.V2
{

	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class Documents : IDocuments
	{
		public Guid BeginCheckin(EphorteIdentity identity, int documentId, int version, string variant)
		{
			throw new NotImplementedException();
		}

		public void WriteCheckinChunk(EphorteIdentity identity, Guid contextId, byte[] contentChunk)
		{
			throw new NotImplementedException();
		}

		public void EndCheckin(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckin(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}

		public Guid BeginCheckout(EphorteIdentity identity, int documentId, int version, string variant)
		{
			throw new NotImplementedException();
		}

		public byte[] ReadCheckoutChunk(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}

		public void EndCheckout(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckout(EphorteIdentity identity, int journalpostId, int documentId, int version)
		{
			throw new NotImplementedException();
		}

		public void EndDocumentRead(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}

		public BeginReadDocumentResponse BeginReadDocument(EphorteIdentity identity,
														   int? documentId,
														   int? version,
														   string variant,
														   int? journalpostId,
														   int? moeteId,
														   int? utvalgsbehandlingId,
														   string sakType,
														   string dokumentType)
		{
			throw new NotImplementedException();
		}

		public byte[] ReadDocumentChunk(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}

		public void CancelReadDocument(EphorteIdentity identity, Guid contextId)
		{
			throw new NotImplementedException();
		}
	}
}