using System;
using System.IO;
using Gecko.NCore.Client.Documents;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
    /// <summary>
    /// 
    /// </summary>
    public static class DocumentManagerExtensions
    {
        /// <summary>
        /// Checks in the <paramref name="content"/> to the specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <param name="content">The content.</param>
        public static void Checkin(this IDocumentManager instance, Dokumentversjon dokumentversjon, Stream content)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            instance.CheckIn(dokumentversjon.DokumentbeskrivelseId, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer, content);
        }

        /// <summary>
        /// Checks out the specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <returns></returns>
        public static Stream Checkout(this IDocumentManager instance, Dokumentversjon dokumentversjon)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            return instance.Checkout(dokumentversjon.DokumentbeskrivelseId, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer);
        }

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="journalpostId">The journalpost id.</param>
        /// <param name="dokumentversjon">The document object.</param>
        public static void CancelCheckout(this IDocumentManager instance, int journalpostId, Dokumentversjon dokumentversjon)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            instance.CancelCheckout(journalpostId, dokumentversjon.DokumentbeskrivelseId, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer);
        }

        /// <summary>
        /// Opens the content stream of specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <returns></returns>
        public static Stream Open(this IDocumentManager instance, Dokumentversjon dokumentversjon)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            return instance.Open(dokumentversjon.DokumentbeskrivelseId, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer);
        }

    	/// <summary>
    	/// Uploads the specified <paramref name="content"/> and attaches it for check in to the specified <paramref name="dokumentversjon"/>.
    	/// </summary>
    	/// <param name="instance">The instance.</param>
    	/// <param name="dokumentversjon">The document object.</param>
    	/// <param name="content">The content.</param>
    	/// <param name="fileName">Name of the file.</param>
    	/// <param name="storageIdentifier"></param>
    	public static void Upload(this IDocumentManager instance, Dokumentversjon dokumentversjon, Stream content, string fileName, string storageIdentifier = "ObjectModelService")
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("fileName");

			if (string.IsNullOrEmpty(storageIdentifier))
				throw new ArgumentNullException("storageIdentifier");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

			if (string.IsNullOrEmpty(dokumentversjon.LagringsformatId))
			{
				var fileFormatId = Path.GetExtension(fileName);
				if (string.IsNullOrEmpty(fileFormatId))
				{
					fileFormatId = "TXT";
				}

				dokumentversjon.LagringsformatId = fileFormatId.Trim('.').ToUpperInvariant();
			}

			var identifier = instance.Upload(content, fileName, storageIdentifier);
			dokumentversjon.Dokumentreferanse = identifier;
        }
    }
   
}
