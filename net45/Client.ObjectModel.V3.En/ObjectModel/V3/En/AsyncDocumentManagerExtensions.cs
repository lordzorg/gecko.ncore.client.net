using System;
using System.IO;
using Gecko.NCore.Client.Documents;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	/// <summary>
	/// 
	/// </summary>
	public static class AsyncDocumentManagerExtensions
	{
        /// <summary>
        /// Checks in the <paramref name="content"/> to the specified <paramref name="documentObject"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="documentObject">The document object.</param>
        /// <param name="content">The content.</param>
        public static async System.Threading.Tasks.Task CheckinAsync(this IAsyncDocumentManager instance, DocumentObject documentObject, Stream content)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (documentObject == null)
                throw new ArgumentNullException("documentObject");

            if (string.IsNullOrEmpty(documentObject.VariantFormatId))
                throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

            await instance.CheckInAsync(documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber, content);
        }

        /// <summary>
        /// Checks out the specified <paramref name="documentObject"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="documentObject">The document object.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<Stream> CheckoutAsync(this IAsyncDocumentManager instance, DocumentObject documentObject)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (documentObject == null)
                throw new ArgumentNullException("documentObject");

            if (string.IsNullOrEmpty(documentObject.VariantFormatId))
                throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

            return await instance.CheckoutAsync(documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber);
        }

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The journalpost id.</param>
        /// <param name="documentObject">The document object.</param>
        public static async System.Threading.Tasks.Task CancelCheckoutAsync(this IAsyncDocumentManager instance, int registryEntryId, DocumentObject documentObject)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (documentObject == null)
                throw new ArgumentNullException("documentObject");

            if (string.IsNullOrEmpty(documentObject.VariantFormatId))
                throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

            await instance.CancelCheckoutAsync(registryEntryId, documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber);
        }

        /// <summary>
        /// Opens the content stream of specified <paramref name="documentObject"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="documentObject">The document object.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<Stream> OpenAsync(this IAsyncDocumentManager instance, DocumentObject documentObject)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (documentObject == null)
                throw new ArgumentNullException("documentObject");

            if (string.IsNullOrEmpty(documentObject.VariantFormatId))
                throw new InvalidOperationException("The DocumentObject.VariantFormatId cannot be <null> or empty.");

            return await instance.OpenAsync(documentObject.DocumentDescriptionId, documentObject.VariantFormatId, documentObject.VersionNumber);
        }

        /// <summary>
        /// Opens the content stream for document given the specified <paramref name="registryEntryId"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<Stream> OpenByRegistryEntryIdAsync(this IAsyncDocumentManager instance, int registryEntryId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (registryEntryId == 0)
                throw new ArgumentOutOfRangeException("registryEntryId");

            return await instance.OpenByRegistryEntryIdAsync(registryEntryId);
        }

        /// <summary>
        /// Opens the content stream for meetingdocument given the specified <paramref name="meetingId"/> and <paramref name="documentType"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="meetingId">The meeting id.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<Stream> OpenMeetingDocumentAsync(this IAsyncDocumentManager instance, int meetingId, string documentType)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (meetingId == 0)
                throw new ArgumentOutOfRangeException("meetingId");

            if (string.IsNullOrEmpty(documentType))
                throw new ArgumentOutOfRangeException("documentType");

            return await instance.OpenMeetingDocumentAsync(meetingId, documentType);
        }

        /// <summary>
        /// Opens the content stream for committee-document handling (document) given the specified <paramref name="dmbHandlingId"/> and <paramref name="caseType"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dmbHandlingId">The meeting id.</param>
        /// <param name="caseType">Type of the document.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<Stream> OpenCommitteeDocumentHandlingAsync(this IAsyncDocumentManager instance, int dmbHandlingId, string caseType)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dmbHandlingId == 0)
                throw new ArgumentOutOfRangeException("dmbHandlingId");

            if (string.IsNullOrEmpty(caseType))
                throw new ArgumentOutOfRangeException("caseType");

            return await instance.OpenCommitteeDocumentHandlingAsync(dmbHandlingId, caseType);
        }

        /// <summary>
        /// Uploads the specified <paramref name="content"/> and attaches it for check in to the specified <paramref name="documentObject"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="documentObject">The document object.</param>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="storageIdentifier">The storage identifier.</param>
        public static async System.Threading.Tasks.Task UploadAsync(this IAsyncDocumentManager instance, DocumentObject documentObject, Stream content, string fileName, string storageIdentifier = "ObjectModelService")
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

            var identifier = await instance.UploadAsync(content, fileName, storageIdentifier);
            documentObject.FilePath = identifier;
        }
    }
   
}
