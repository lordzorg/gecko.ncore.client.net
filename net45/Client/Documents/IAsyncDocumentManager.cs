using System.IO;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Documents
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAsyncDocumentManager: IDocumentManager
	{
        /// <summary>
        /// Checks in the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="versionNumber">The version.</param>
        /// <param name="content">The content.</param>
        Task CheckInAsync(int documentDescriptionId, string variant, int versionNumber, Stream content);

        /// <summary>
        /// Checks out the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="versionNumber">The version.</param>
        /// <returns></returns>
        Task<Stream> CheckoutAsync(int documentDescriptionId, string variant, int versionNumber);

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="registryEntryId">The journalpost id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        Task CancelCheckoutAsync(int registryEntryId, int documentDescriptionId, string variant, int version);

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="meetingDocumentId">The meeting document id.</param>
        /// <param name="committeeHandlingDocumentId">The committee handling document id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        Task CancelCheckoutAsync(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId, int documentDescriptionId, string variant, int version);

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="versionNumber">The version.</param>
        /// <returns></returns>
        Task<Stream> OpenAsync(int documentDescriptionId, string variant, int versionNumber);

        /// <summary>
        /// Opens the specified document by registryentry id.
        /// </summary>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <returns></returns>
        Task<Stream> OpenByRegistryEntryIdAsync(int registryEntryId);

        /// <summary>
        /// Opens the specified meeting document.
        /// </summary>
        /// <param name="meetingId">The meeting id.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <returns></returns>
        Task<Stream> OpenMeetingDocumentAsync(int meetingId, string documentType);

        /// <summary>
        /// Opens the specified committee-document handling
        /// </summary>
        /// <param name="dmbHandlingId">The DMB handling id.</param>
        /// <param name="caseType">Type of the case.</param>
        /// <returns></returns>
        Task<Stream> OpenCommitteeDocumentHandlingAsync(int dmbHandlingId, string caseType);

        /// <summary>
        /// Uploads the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="storageIdentifier">The storage identifier.</param>
        /// <returns>A unique identifier for the file.</returns>
        Task<string> UploadAsync(Stream content, string fileName, string storageIdentifier);

        /// <summary>
        /// Uploads the temporary.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task<string> UploadTemporaryAsync(Stream content, string fileName);
    }
}