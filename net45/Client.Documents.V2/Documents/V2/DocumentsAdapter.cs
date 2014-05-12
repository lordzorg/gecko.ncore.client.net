using System;
using System.IO;

namespace Gecko.NCore.Client.Documents.V2
{
	public class DocumentsAdapter : ServiceAdapterBase<DocumentsClient>, IDocumentsAdapter
	{
		private readonly EphorteContextIdentity _contextIdentity;

		public DocumentsAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
			: base(settings)
		{
			_contextIdentity = contextIdentity;
		}

		private EphorteIdentity CreateEphorteIdentity()
		{
			return new EphorteIdentity
			{
				UserName = _contextIdentity.Username,
				Password = _contextIdentity.Password,
				Role = _contextIdentity.Role,
				Database = _contextIdentity.Database,
				ExternalSystemName = _contextIdentity.ExternalSystemName
			};
		}

		private Stream GetDocument(Func<Documents, EphorteIdentity, BeginReadDocumentResponse> getDocument)
		{
			var ephorteIdentity = CreateEphorteIdentity();
			var client = CreateServiceClient();
			var beginRead = getDocument(client, ephorteIdentity);
			return new ChunkStream(client, ephorteIdentity, beginRead.ContextId, beginRead.ContentLength);
		}

		public void CheckIn(int documentDescriptionId, string variant, int version, Stream content)
		{
			throw new NotImplementedException();
		}

		public Stream Checkout(int documentDescriptionId, string variant, int version)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckout(int registryEntryId, int documentDescriptionId, string variant, int version)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckout(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId,
			int documentDescriptionId, string variant, int version)
		{
			throw new NotImplementedException();
		}

		public Stream Open(int documentDescriptionId, string variant, int version)
		{
			return GetDocument((client, identity) => 
				client.BeginReadDocument(identity, documentDescriptionId, version, variant, null, null, null, null, null));
		}

		public Stream OpenByRegistryEntryId(int registryEntryId)
		{
			return GetDocument((client, identity) => 
				client.BeginReadDocument(identity, null, null, null, registryEntryId, null, null, null, null));
		}

		public Stream OpenMeetingDocument(int meetingId, string documentType)
		{
			return GetDocument((client, identity) =>
				client.BeginReadDocument(identity, null, null, null, null, meetingId, null, null, documentType));
		}

		public Stream OpenCommitteeDocumentHandling(int dmbHandlingId, string caseType)
		{
			return GetDocument((client, identity) =>
				client.BeginReadDocument(identity, null, null, null, null, null, dmbHandlingId, caseType, null));
		}

		public string UploadToNamedStorage(Stream content, string fileName, string storageIdentifier)
		{
			throw new NotImplementedException();
		}

		public string UploadToTemporaryStorage(Stream content, string fileName)
		{
			throw new NotImplementedException();
		}
	}
}
