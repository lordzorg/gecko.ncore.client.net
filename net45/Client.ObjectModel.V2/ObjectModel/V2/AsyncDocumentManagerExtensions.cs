using System;
using System.IO;
using System.Threading.Tasks;
using Gecko.NCore.Client.Documents;

namespace Gecko.NCore.Client.ObjectModel.V2
{
    /// <summary>
    /// 
    /// </summary>
    public static class AsyncDocumentManagerExtensions
    {
        /// <summary>
        /// Checks in the <paramref name="content"/> to the specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <param name="content">The content.</param>
        public static async Task CheckinAsync(this IAsyncDocumentManager instance, Dokumentversjon dokumentversjon, Stream content)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            await instance.CheckInAsync(dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value, content);
        }

        /// <summary>
        /// Checks out the specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <returns></returns>
        public static async Task<Stream> CheckoutAsync(this IAsyncDocumentManager instance, Dokumentversjon dokumentversjon)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            return await instance.CheckoutAsync(dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value);
        }

        /// <summary>
        /// Cancels the checkout.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="journalpostId">The journalpost id.</param>
        /// <param name="dokumentversjon">The document object.</param>
        public static async Task CancelCheckoutAsync(this IAsyncDocumentManager instance, int journalpostId, Dokumentversjon dokumentversjon)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            await instance.CancelCheckoutAsync(journalpostId, dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value);
        }

        /// <summary>
        /// Opens the content stream of specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <returns></returns>
        public static async Task<Stream> OpenAsync(this IAsyncDocumentManager instance, Dokumentversjon dokumentversjon)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            return await instance.OpenAsync(dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value);
        }

        /// <summary>
        /// Uploads the specified <paramref name="content"/> and attaches it for check in to the specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        public static async Task UploadAsync(this IAsyncDocumentManager instance, Dokumentversjon dokumentversjon, Stream content, string fileName)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            var identifier = await instance.UploadAsync(content, fileName, null);
            dokumentversjon.Dokumentreferanse = identifier;
        }
    }
   
}
