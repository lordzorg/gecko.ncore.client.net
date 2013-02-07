using System.IO;

namespace Gecko.NCore.Client.Documents
{
	/// <summary>
	/// 
	/// </summary>
	public class DocumentManager : IDocumentManager
	{
		private readonly IDocumentsAdapter _documentsAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentManager"/> class.
		/// </summary>
		/// <param name="documentsAdapter">The documents adapter.</param>
		internal DocumentManager(IDocumentsAdapter documentsAdapter)
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
		public void CheckIn(int documentDescriptionId, string variant, int versionNumber, Stream content)
		{
			_documentsAdapter.CheckIn(documentDescriptionId, variant, versionNumber, content);
		}

		/// <summary>
		/// Checks out the specified document.
		/// </summary>
		/// <param name="documentDescriptionId">The document description id.</param>
		/// <param name="variant">The variant.</param>
		/// <param name="versionNumber">The version.</param>
		/// <returns></returns>
		public Stream Checkout(int documentDescriptionId, string variant, int versionNumber)
		{
			return _documentsAdapter.Checkout(documentDescriptionId, variant, versionNumber);
		}

		/// <summary>
		/// Cancels the checkout.
		/// </summary>
		/// <param name="registryEntryId">The journalpost id.</param>
		/// <param name="documentDescriptionId">The document description id.</param>
		/// <param name="variant">The variant.</param>
		/// <param name="version">The version.</param>
		public void CancelCheckout(int registryEntryId, int documentDescriptionId, string variant, int version)
		{
			_documentsAdapter.CancelCheckout(registryEntryId, documentDescriptionId, variant, version);
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
		public void CancelCheckout(int registryEntryId, int meetingDocumentId, int committeeHandlingDocumentId, int documentDescriptionId, string variant, int version)
		{
			_documentsAdapter.CancelCheckout(registryEntryId, meetingDocumentId, committeeHandlingDocumentId, documentDescriptionId, variant, version);
		}

		/// <summary>
		/// Opens the specified document.
		/// </summary>
		/// <param name="documentDescriptionId">The document description id.</param>
		/// <param name="variant">The variant.</param>
		/// <param name="versionNumber">The version.</param>
		/// <returns></returns>
		public Stream Open(int documentDescriptionId, string variant, int versionNumber)
		{
			return _documentsAdapter.Open(documentDescriptionId, variant, versionNumber);
		}

		public Stream OpenByRegistryEntryId(int registryEntryId)
		{
			return _documentsAdapter.OpenByRegistryEntryId(registryEntryId);
		}

		public Stream OpenMeetingDocument(int meetingId, string documentType)
		{
			return _documentsAdapter.OpenMeetingDocument(meetingId, documentType);
		}

		public Stream OpenCommitteeDocumentHandling(int dmbHandlingId, string caseType)
		{
			return _documentsAdapter.OpenCommitteeDocumentHandling(dmbHandlingId, caseType);
		}

		/// <summary>
		/// Uploads the specified content.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="storageIdentifier">The storage identifier.</param>
		/// <returns>A unique identifier for the file.</returns>
		public string Upload(Stream content, string fileName, string storageIdentifier)
		{
			return _documentsAdapter.UploadToNamedStorage(content, fileName, storageIdentifier);
		}

		/// <summary>
		/// Uploads the temporary.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public string UploadTemporary(Stream content, string fileName)
		{
			return _documentsAdapter.UploadToTemporaryStorage(content, fileName);
		}
	}
}
