using System.Diagnostics;
using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Id: {Id}, Reference: {RegisterYear}/{SequenceNumber}-{DocumentNumber}, Type: {RegistryEntryTypeId}")]
    public partial class RegistryEntry
    {
        private IDataObjectCollection<SenderRecipient> _senderRecipients;

        /// <summary>
        /// Gets the classifications.
        /// </summary>
        /// <value>The classifications.</value>
        public IDataObjectCollection<SenderRecipient> SenderRecipients
        {
            get { return _senderRecipients ?? (_senderRecipients = new TypedDataObjectCollection<SenderRecipient>(x => x.RegistryEntryId == Id)); }
        }

        private IDataObjectCollection<RegistryEntryDocument> _registryEntryDocuments;

        /// <summary>
        /// Gets the registry entry documents.
        /// </summary>
        /// <value>The registry entry documents.</value>
        public IDataObjectCollection<RegistryEntryDocument> RegistryEntryDocuments
        {
            get { return _registryEntryDocuments ?? (_registryEntryDocuments = new TypedDataObjectCollection<RegistryEntryDocument>(x => x.RegistryEntryId == Id)); }
        }

        private IDataObjectCollection<Remark> _remarks;

        /// <summary>
        /// Gets the remarks.
        /// </summary>
        /// <value>The remarks.</value>
        public IDataObjectCollection<Remark> Remarks
        {
            get { return _remarks ?? (_remarks = new TypedDataObjectCollection<Remark>(x => x.RegistryEntryId == Id)); }
        }


        private IDataObjectCollection<RegistryEntryLink> _links;

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        public IDataObjectCollection<RegistryEntryLink> Links
        {
            get { return _links ?? (_links = new TypedDataObjectCollection<RegistryEntryLink>(x => x.RegistryEntryId == Id)); }
        }
    }
}
