using System.IO;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Documents
{
	/// <summary>
	/// 
	/// </summary>
	public class AsyncDocumentManager : DocumentManager, IAsyncDocumentManager
	{
		private readonly IAsyncDocumentsAdapter _documentsAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncDocumentManager"/> class.
		/// </summary>
		/// <param name="documentsAdapter">The documents adapter.</param>
		internal AsyncDocumentManager(IAsyncDocumentsAdapter documentsAdapter)
            : base(documentsAdapter)
		{
			_documentsAdapter = documentsAdapter;
		}

	    public async Task CheckInAsync(int documentDescriptionId, string variant, int versionNumber, Stream content)
	    {
            await _documentsAdapter.CheckInAsync(documentDescriptionId, variant, versionNumber, content);
        }

	    public async Task<Stream> CheckoutAsync(int documentDescriptionId, string variant, int versionNumber)
	    {
            return await _documentsAdapter.CheckoutAsync(documentDescriptionId, variant, versionNumber);
        }

	    public async Task CancelCheckoutAsync(int registryEntryId, int documentDescriptionId, string variant, int version)
	    {
            await _documentsAdapter.CancelCheckoutAsync(registryEntryId, documentDescriptionId, variant, version);
	    }

	    public async Task CancelCheckoutAsync(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId,int documentDescriptionId, string variant, int version)
	    {
            await _documentsAdapter.CancelCheckoutAsync(registryEntryId, meetingDocumentId, committeeHandlingDocumentId, documentDescriptionId, variant, version);
        }

	    public async Task<Stream> OpenAsync(int documentDescriptionId, string variant, int versionNumber)
	    {
            return await _documentsAdapter.OpenAsync(documentDescriptionId, variant, versionNumber);
        }

	    public async Task<Stream> OpenByRegistryEntryIdAsync(int registryEntryId)
	    {
            return await _documentsAdapter.OpenByRegistryEntryIdAsync(registryEntryId);
        }

	    public async Task<Stream> OpenMeetingDocumentAsync(int meetingId, string documentType)
	    {
            return await _documentsAdapter.OpenMeetingDocumentAsync(meetingId, documentType);
        }

	    public async Task<Stream> OpenCommitteeDocumentHandlingAsync(int dmbHandlingId, string caseType)
	    {
            return await _documentsAdapter.OpenCommitteeDocumentHandlingAsync(dmbHandlingId, caseType);
        }

	    public async Task<string> UploadAsync(Stream content, string fileName, string storageIdentifier)
	    {
            return await _documentsAdapter.UploadToNamedStorageAsync(content, fileName, storageIdentifier);
        }

	    public async Task<string> UploadTemporaryAsync(Stream content, string fileName)
	    {
            return await _documentsAdapter.UploadToTemporaryStorageAsync(content, fileName);
        }
	}
}
