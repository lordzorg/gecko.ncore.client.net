using System.IO;
using System.ServiceModel;

namespace Gecko.NCore.Client.Documents.V3
{
	public class DocumentsAdapter : ServiceAdapterBase<DocumentServiceClient>, IDocumentsAdapter
	{
		private readonly EphorteContextIdentity _contextIdentity;

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentsAdapter"/> class.
		/// </summary>
		/// <param name="contextIdentity">The identity.</param>
		/// <param name="settings">The configuration.</param>
		public DocumentsAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(settings)
		{
			_contextIdentity = contextIdentity;
		}

	    protected EphorteIdentity CreateEphorteIdentity()
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

		public void CheckIn(int documentDescriptionId, string variant, int version, Stream content)
		{
			var request = new CheckinMessage
			{
				DocumentCriteria = new DocumentCriteria
				{
					EphorteIdentity = CreateEphorteIdentity(),
					DocumentId = documentDescriptionId,
					Variant = variant,
					Version = version
				},
				Content = content
			};

			using (var documentsService = CreateServiceClient())
			{
				documentsService.Checkin(request);
			}
		}

		public Stream Checkout(int documentDescriptionId, string variant = "P", int version = -1)
		{
			var request = new CheckoutRequest(documentDescriptionId, CreateEphorteIdentity(), variant, version);
			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.Checkout(request);
				return response.Content;
			}
		}

		public void CancelCheckout(int registryEntryId, int documentDescriptionId, string variant = "P", int version = -1)
		{
			var request = new CancelCheckoutRequest
			{
				JournalpostId = registryEntryId,
				DocumentId = documentDescriptionId,
				Variant = variant,
				Version = version,
				Identity = CreateEphorteIdentity()
			};

			using (var documentsService = CreateServiceClient())
			{
				documentsService.CancelCheckout(request);
			}
		}

		public void CancelCheckout(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId, int documentDescriptionId, string variant = "P", int version = -1)
		{
			var request = new CancelCheckoutRequest
			{
				JournalpostId = registryEntryId,
				MeetingDocumentId = meetingDocumentId,
				CommitteeHandlingDocumentId = committeeHandlingDocumentId,
				DocumentId = documentDescriptionId,
				Variant = variant,
				Version = version,
				Identity = CreateEphorteIdentity()
			};

			using (var documentsService = CreateServiceClient())
			{
				documentsService.CancelCheckout(request);
			}
		}

		public Stream Open(int documentDescriptionId, string variant = "P", int version = -1)
		{
			var request = new GetDocumentContentMessage(documentDescriptionId, CreateEphorteIdentity(), variant, version);

			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.GetDocumentContentBase(request);
				return response.Content;
			}
		}

		public Stream OpenByRegistryEntryId(int registryEntryId)
		{
			var request = new GetJournalpostDocumentContentMessage(CreateEphorteIdentity(), registryEntryId);

			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.GetDocumentContentByJournalPostId(request);
				return response.Content;
			}
		}

		public Stream OpenMeetingDocument(int meetingId, string documentType)
		{
			var request = new GetMoeteDocumentContentMessage(documentType, CreateEphorteIdentity(), meetingId);

			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.GetDocumentContentByMoId(request);
				return response.Content;
			}
		}

		public Stream OpenCommitteeDocumentHandling(int dmbHandlingId, string caseType)
		{
			var request = new GetUtvalgDocumentContentMessage(CreateEphorteIdentity(), caseType, dmbHandlingId);

			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.GetDocumentContentByUbId(request);
				return response.Content;
			}
		}

		public string UploadToTemporaryStorage(Stream content, string fileName)
		{
			var request = new UploadMessage(CreateEphorteIdentity(), fileName, null, content);

			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.UploadFile(request);
				return response.Identifier;
			}
		}

		public string UploadToNamedStorage(Stream content, string fileName, string storageIdentifier)
		{
			var request = new UploadMessage(CreateEphorteIdentity(), fileName, storageIdentifier, content);

			using (var documentsService = CreateServiceClient())
			{
				var response = documentsService.UploadFile(request);
				return response.FileName;
			}
		}
	}
}
