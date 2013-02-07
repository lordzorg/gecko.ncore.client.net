using System.IO;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Documents.V3
{
	public class AsyncDocumentsAdapter : DocumentsAdapter, IAsyncDocumentsAdapter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentsAdapter"/> class.
		/// </summary>
		/// <param name="contextIdentity">The identity.</param>
		/// <param name="settings">The configuration.</param>
		public AsyncDocumentsAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(contextIdentity, settings)
		{
		}

	    public async Task CheckInAsync(int documentDescriptionId, string variant, int version, Stream content)
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
                await documentsService.CheckinAsync(request);
            }
        }

	    public async Task<Stream> CheckoutAsync(int documentDescriptionId, string variant, int version)
	    {
            var request = new CheckoutRequest(documentDescriptionId, CreateEphorteIdentity(), variant, version);
            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.CheckoutAsync(request);
                return response.Content;
            }
        }

	    public async Task CancelCheckoutAsync(int registryEntryId, int documentDescriptionId, string variant, int version)
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
                await documentsService.CancelCheckoutAsync(request);
            }
        }

	    public async Task CancelCheckoutAsync(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId,
	                                    int documentDescriptionId, string variant, int version)
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
                await documentsService.CancelCheckoutAsync(request);
            }
        }

	    public async Task<Stream> OpenAsync(int documentDescriptionId, string variant, int version)
	    {
            var request = new GetDocumentContentMessage(documentDescriptionId, CreateEphorteIdentity(), variant, version);

            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.GetDocumentContentBaseAsync(request);
                return response.Content;
            }
        }

	    public async Task<Stream> OpenByRegistryEntryIdAsync(int registryEntryId)
	    {
            var request = new GetJournalpostDocumentContentMessage(CreateEphorteIdentity(), registryEntryId);

            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.GetDocumentContentByJournalPostIdAsync(request);
                return response.Content;
            }
        }

	    public async Task<Stream> OpenMeetingDocumentAsync(int meetingId, string documentType)
	    {
            var request = new GetMoeteDocumentContentMessage(documentType, CreateEphorteIdentity(), meetingId);

            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.GetDocumentContentByMoIdAsync(request);
                return response.Content;
            }
        }

	    public async Task<Stream> OpenCommitteeDocumentHandlingAsync(int dmbHandlingId, string caseType)
	    {
            var request = new GetUtvalgDocumentContentMessage(CreateEphorteIdentity(), caseType, dmbHandlingId);

            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.GetDocumentContentByUbIdAsync(request);
                return response.Content;
            }
        }

	    public async Task<string> UploadToNamedStorageAsync(Stream content, string fileName, string storageIdentifier)
	    {
            var request = new UploadMessage(CreateEphorteIdentity(), fileName, storageIdentifier, content);

            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.UploadFileAsync(request);
                return response.FileName;
            }
        }

	    public async Task<string> UploadToTemporaryStorageAsync(Stream content, string fileName)
	    {
            var request = new UploadMessage(CreateEphorteIdentity(), fileName, null, content);

            using (var documentsService = CreateServiceClient())
            {
                var response = await documentsService.UploadFileAsync(request);
                return response.Identifier;
            }
        }
	}
}
