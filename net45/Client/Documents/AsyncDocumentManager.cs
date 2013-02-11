using System.IO;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Documents
{
    /// <summary>
    /// Class AsyncDocumentManager
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

        /// <summary>
        /// Checks in the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="versionNumber">The version.</param>
        /// <param name="content">The content.</param>
        /// <returns>Task.</returns>
	    public async Task CheckInAsync(int documentDescriptionId, string variant, int versionNumber, Stream content)
	    {
            await _documentsAdapter.CheckInAsync(documentDescriptionId, variant, versionNumber, content);
        }

        /// <summary>
        /// Checks out the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="versionNumber">The version.</param>
        /// <returns>Task{Stream}.</returns>
	    public async Task<Stream> CheckoutAsync(int documentDescriptionId, string variant, int versionNumber)
	    {
            return await _documentsAdapter.CheckoutAsync(documentDescriptionId, variant, versionNumber);
        }

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="registryEntryId">The journalpost id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        /// <returns>Task.</returns>
	    public async Task CancelCheckoutAsync(int registryEntryId, int documentDescriptionId, string variant, int version)
	    {
            await _documentsAdapter.CancelCheckoutAsync(registryEntryId, documentDescriptionId, variant, version);
	    }

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="meetingDocumentId">The meeting document id.</param>
        /// <param name="committeeHandlingDocumentId">The committee handling document id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        /// <returns>Task.</returns>
	    public async Task CancelCheckoutAsync(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId,int documentDescriptionId, string variant, int version)
	    {
            await _documentsAdapter.CancelCheckoutAsync(registryEntryId, meetingDocumentId, committeeHandlingDocumentId, documentDescriptionId, variant, version);
        }

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="versionNumber">The version.</param>
        /// <returns>Task{Stream}.</returns>
	    public async Task<Stream> OpenAsync(int documentDescriptionId, string variant, int versionNumber)
	    {
            return await _documentsAdapter.OpenAsync(documentDescriptionId, variant, versionNumber);
        }

        /// <summary>
        /// Opens the specified document by registryentry id.
        /// </summary>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <returns>Task{Stream}.</returns>
	    public async Task<Stream> OpenByRegistryEntryIdAsync(int registryEntryId)
	    {
            return await _documentsAdapter.OpenByRegistryEntryIdAsync(registryEntryId);
        }

        /// <summary>
        /// Opens the specified meeting document.
        /// </summary>
        /// <param name="meetingId">The meeting id.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <returns>Task{Stream}.</returns>
	    public async Task<Stream> OpenMeetingDocumentAsync(int meetingId, string documentType)
	    {
            return await _documentsAdapter.OpenMeetingDocumentAsync(meetingId, documentType);
        }

        /// <summary>
        /// Opens the specified committee-document handling
        /// </summary>
        /// <param name="dmbHandlingId">The DMB handling id.</param>
        /// <param name="caseType">Type of the case.</param>
        /// <returns>Task{Stream}.</returns>
	    public async Task<Stream> OpenCommitteeDocumentHandlingAsync(int dmbHandlingId, string caseType)
	    {
            return await _documentsAdapter.OpenCommitteeDocumentHandlingAsync(dmbHandlingId, caseType);
        }

        /// <summary>
        /// Uploads the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="storageIdentifier">The storage identifier.</param>
        /// <returns>A unique identifier for the file.</returns>
	    public async Task<string> UploadAsync(Stream content, string fileName, string storageIdentifier)
	    {
            return await _documentsAdapter.UploadToNamedStorageAsync(content, fileName, storageIdentifier);
        }

        /// <summary>
        /// Uploads the temporary.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Task{System.String}.</returns>
	    public async Task<string> UploadTemporaryAsync(Stream content, string fileName)
	    {
            return await _documentsAdapter.UploadToTemporaryStorageAsync(content, fileName);
        }
	}
}
