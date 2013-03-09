using System;
using System.IO;
using Gecko.NCore.Client.Documents;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	/// <summary>
	/// 
	/// </summary>
	public static class DocumentManagerExtensions
	{
		/// <summary>
		/// Checks in the <paramref name="content"/> to the specified <paramref name="documentObject"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="documentObject">The document object.</param>
		/// <param name="content">The content.</param>
		public static void Checkin(this IDocumentManager instance, DocumentObject documentObject, Stream content)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (documentObject == null)
				throw new ArgumentNullException("documentObject");

			if (string.IsNullOrEmpty(documentObject.VariantFormatId))
				throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

			instance.CheckIn(documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber, content);
		}

		/// <summary>
		/// Checks out the specified <paramref name="documentObject"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="documentObject">The document object.</param>
		/// <returns></returns>
		public static Stream Checkout(this IDocumentManager instance, DocumentObject documentObject)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (documentObject == null)
				throw new ArgumentNullException("documentObject");

			if (string.IsNullOrEmpty(documentObject.VariantFormatId))
				throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

			return instance.Checkout(documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber);
		}

		/// <summary>
		/// Cancels the checkout.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="registryEntryId">The journalpost id.</param>
		/// <param name="documentObject">The document object.</param>
		public static void CancelCheckout(this IDocumentManager instance, int registryEntryId, DocumentObject documentObject)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (documentObject == null)
				throw new ArgumentNullException("documentObject");

			if (string.IsNullOrEmpty(documentObject.VariantFormatId))
				throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

			instance.CancelCheckout(registryEntryId, documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber);
		}

		/// <summary>
		/// Opens the content stream of specified <paramref name="documentObject"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="documentObject">The document object.</param>
		/// <returns></returns>
		public static Stream Open(this IDocumentManager instance, DocumentObject documentObject)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (documentObject == null)
				throw new ArgumentNullException("documentObject");

			if (string.IsNullOrEmpty(documentObject.VariantFormatId))
				throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

			return instance.Open(documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber);
		}

		/// <summary>
		/// Opens the content stream for document given the specified <paramref name="registryEntryId"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="registryEntryId">The registry entry id.</param>
		/// <returns></returns>
		public static Stream OpenByRegistryEntryId(this IDocumentManager instance, int registryEntryId)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (registryEntryId == 0)
				throw new ArgumentOutOfRangeException("registryEntryId");

			return instance.OpenByRegistryEntryId(registryEntryId);
		}

		/// <summary>
		/// Opens the content stream for meetingdocument given the specified <paramref name="meetingId"/> and <paramref name="documentType"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="meetingId">The meeting id.</param>
		/// <param name="documentType">Type of the document.</param>
		/// <returns></returns>
		public static Stream OpenMeetingDocument(this IDocumentManager instance, int meetingId, string documentType)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (meetingId == 0)
				throw new ArgumentOutOfRangeException("meetingId");

			if (string.IsNullOrEmpty(documentType))
				throw new ArgumentOutOfRangeException("documentType");

			return instance.OpenMeetingDocument(meetingId, documentType);
		}

		/// <summary>
		/// Opens the content stream for committee-document handling (document) given the specified <paramref name="dmbHandlingId"/> and <paramref name="caseType"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="dmbHandlingId">The meeting id.</param>
		/// <param name="caseType">Type of the document.</param>
		/// <returns></returns>
		public static Stream OpenCommitteeDocumentHandling(this IDocumentManager instance, int dmbHandlingId, string caseType)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (dmbHandlingId == 0)
				throw new ArgumentOutOfRangeException("dmbHandlingId");

			if (string.IsNullOrEmpty(caseType))
				throw new ArgumentOutOfRangeException("caseType");

			return instance.OpenCommitteeDocumentHandling(dmbHandlingId, caseType);
		}

		/// <summary>
		/// Uploads the specified <paramref name="content"/> and attaches it for check in to the specified <paramref name="documentObject"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="documentObject">The document object.</param>
		/// <param name="content">The content.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="storageIdentifier">The storage identifier.</param>
		public static void Upload(this IDocumentManager instance, DocumentObject documentObject, Stream content, string fileName, string storageIdentifier = "ObjectModelService")
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			if (documentObject == null)
				throw new ArgumentNullException("documentObject");

			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("fileName");

			if (string.IsNullOrEmpty(storageIdentifier))
				throw new ArgumentNullException("storageIdentifier");

			if (string.IsNullOrEmpty(documentObject.VariantFormatId))
				throw new ArgumentException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

			if (string.IsNullOrEmpty(documentObject.FileformatId))
			{
				var fileFormatId = Path.GetExtension(fileName);
				if (string.IsNullOrEmpty(fileFormatId))
				{
					fileFormatId = "TXT";
				}

				documentObject.FileformatId = fileFormatId.Trim('.').ToUpperInvariant();
			}

			var identifier = instance.Upload(content, fileName, storageIdentifier);
			documentObject.FilePath = identifier;
		}
    }
   
}
