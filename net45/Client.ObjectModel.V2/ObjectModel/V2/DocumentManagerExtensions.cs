using System;
using System.IO;
using Gecko.NCore.Client.Documents;

namespace Gecko.NCore.Client.ObjectModel.V2
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

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            instance.CheckIn(dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value, content);
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

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            return instance.Checkout(dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value);
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

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            instance.CancelCheckout(journalpostId, dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value);
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

            if (!dokumentversjon.DokumentbeskrivelseId.HasValue)
                throw new InvalidOperationException("The Documentversjon.DokumentbeskrivelseId cannot be <null>.");

            if (string.IsNullOrEmpty(dokumentversjon.VariantId))
                throw new InvalidOperationException("The Documentversjon.VariantId cannot be <null> or empty.");

            if (!dokumentversjon.Versjonsnummer.HasValue)
                throw new InvalidOperationException("The Documentversjon.Versjonsnummer cannot be <null>.");

            return instance.Open(dokumentversjon.DokumentbeskrivelseId.Value, dokumentversjon.VariantId, dokumentversjon.Versjonsnummer.Value);
        }

        /// <summary>
        /// Uploads the specified <paramref name="content"/> and attaches it for check in to the specified <paramref name="dokumentversjon"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dokumentversjon">The document object.</param>
        /// <param name="content">The content.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void Upload(this IDocumentManager instance, Dokumentversjon dokumentversjon, Stream content, string fileName)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (dokumentversjon == null)
                throw new ArgumentNullException("dokumentversjon");

            var identifier = instance.Upload(content, fileName, null);
            dokumentversjon.Dokumentreferanse = identifier;
        }
    }
   
}
