using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	public partial class GeographicalObject
	{
		private TypedDataObjectCollection<GeographicalObjectLink> _caseLinks;
		private TypedDataObjectCollection<GeographicalObjectLink> _registryEntryLinks;
		private TypedDataObjectCollection<GeographicalObjectLink> _casePartLinks;
		private TypedDataObjectCollection<GeographicalObjectLink> _senderRecipientLinks;
		private TypedDataObjectCollection<GeographicalObjectLink> _links;

		/// <summary>
		/// Gets the journalposter.
		/// </summary>
		/// <value>The journalposter.</value>
		public IDataObjectCollection<GeographicalObjectLink> CaseLinks
		{
			get { return _caseLinks ?? (_caseLinks = new TypedDataObjectCollection<GeographicalObjectLink>(x => x.GeographicalObjectId == Id && x.GeographicalEntityId == 3)); }
		}

		public IDataObjectCollection<GeographicalObjectLink> RegistryEntryLinks
		{
			get { return _registryEntryLinks ?? (_registryEntryLinks = new TypedDataObjectCollection<GeographicalObjectLink>(x => x.GeographicalObjectId == Id && x.GeographicalEntityId == 2)); }
		}
		
		public IDataObjectCollection<GeographicalObjectLink> CasePartLinks
		{
			get { return _casePartLinks ?? (_casePartLinks = new TypedDataObjectCollection<GeographicalObjectLink>(x => x.GeographicalObjectId == Id && x.GeographicalEntityId == 4)); }
		}
		
		public IDataObjectCollection<GeographicalObjectLink> SenderRecipientLinks
		{
			get { return _senderRecipientLinks ?? (_senderRecipientLinks = new TypedDataObjectCollection<GeographicalObjectLink>(x => x.GeographicalObjectId == Id && x.GeographicalEntityId == 1)); }
		}
		
		public IDataObjectCollection<GeographicalObjectLink> Links
		{
			get { return _links ?? (_links = new TypedDataObjectCollection<GeographicalObjectLink>(x => x.GeographicalObjectId == Id)); }
		}
	}
}
