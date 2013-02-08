using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts;
using Ephorte.ServiceModel.Contracts.Documents.V1;


namespace Gecko.NCore.Host.Services.Documents.V1
{
	
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class DocumentsService : IDocumentsService
	{
		public void Checkin(CheckinMessage message)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckout(EphorteIdentity identity, int journalpostId, int documentId, int version)
		{
			throw new NotImplementedException();
		}

		public Stream Checkout(EphorteIdentity identity, int documentId, int version, string variant)
		{
			throw new NotImplementedException();
		}

		public Stream GetDocumentContent(EphorteIdentity identity, int documentId, int version, string variant)
		{
			throw new NotImplementedException();
		}

		public Stream GetDocumentContent(EphorteIdentity identity, int documentId)
		{
			throw new NotImplementedException();
		}

		public Stream GetDocumentContentByDokBeskrivId(EphorteIdentity identity, int dokumentId, int version, string variant)
		{
			throw new NotImplementedException();
		}

		public Stream GetDocumentContentByJournalPostId(EphorteIdentity identity, int journalpostId)
		{
			throw new NotImplementedException();
		}

		public Stream GetDocumentContentByMoId(EphorteIdentity identity, int moId, string documentType)
		{
			throw new NotImplementedException();
		}

		public Stream GetDocumentContentByUbId(EphorteIdentity identity, int ubId, string sakType)
		{
			throw new NotImplementedException();
		}

		public string GetDocumentFileName(EphorteIdentity identity, int documentId, int version, string variant)
		{
			throw new NotImplementedException();
		}
	}
}