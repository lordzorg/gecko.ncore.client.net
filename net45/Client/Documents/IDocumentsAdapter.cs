using System.IO;

namespace Gecko.NCore.Client.Documents
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentsAdapter
    {
        /// <summary>
        /// Checks in the specified document.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        /// <param name="content">The content.</param>
        void CheckIn(int documentDescriptionId, string variant, int version, Stream content);

        /// <summary>
        /// Checks out the specified document description id.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        Stream Checkout(int documentDescriptionId, string variant, int version);

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        void CancelCheckout(int registryEntryId, int documentDescriptionId, string variant, int version);

		  /// <summary>
		  /// Cancels the checkout.
		  /// </summary>
		  /// <param name="registryEntryId">The registry entry id.</param>
		  /// <param name="meetingDocumentId">The meeting document id.</param>
		  /// <param name="committeeHandlingDocumentId">The committee handling document id.</param>
		  /// <param name="documentDescriptionId">The document description id.</param>
		  /// <param name="variant">The variant.</param>
		  /// <param name="version">The version.</param>
		  void CancelCheckout(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId, int documentDescriptionId, string variant, int version );

		  /// <summary>
        /// Opens the specified document description id.
        /// </summary>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="variant">The variant.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        Stream Open(int documentDescriptionId, string variant, int version);

		  /// <summary>
		  /// Opens the specified document by registryentry id.
		  /// </summary>
		  /// <param name="registryEntryId">The registry entry id.</param>
		  /// <returns></returns>
		  Stream OpenByRegistryEntryId(int registryEntryId);

		  /// <summary>
		  /// Opens the specified meeting document.
		  /// </summary>
		  /// <param name="meetingId">The meeting id.</param>
		  /// <param name="documentType">Type of the document.</param>
		  /// <returns></returns>
		  Stream OpenMeetingDocument(int meetingId, string documentType);

		  /// <summary>
		  /// Opens the specified committee-document-handling
		  /// </summary>
		  /// <param name="dmbHandlingId">The DMB handling id.</param>
		  /// <param name="caseType">Type of the case.</param>
		  /// <returns></returns>
		  Stream OpenCommitteeDocumentHandling(int dmbHandlingId, string caseType);
		  
		 /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="storageIdentifier">The storage identifier.</param>
        /// <returns></returns>
        string UploadToNamedStorage(Stream content, string fileName, string storageIdentifier);

		/// <summary>
		/// Uploads to temporary storage.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		string UploadToTemporaryStorage(Stream content, string fileName);
    }
}