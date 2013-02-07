using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
	public partial class GeografiskObjekt
	{
		private TypedDataObjectCollection<GeografiskObjektLink> _saksLenker;
		private TypedDataObjectCollection<GeografiskObjektLink> _journalpostLenker;
		private TypedDataObjectCollection<GeografiskObjektLink> _sakspartLenker;
		private TypedDataObjectCollection<GeografiskObjektLink> _avsenderMottakerLenker;
		private TypedDataObjectCollection<GeografiskObjektLink> _lenker;

		/// <summary>
		/// Gets the journalposter.
		/// </summary>
		/// <value>The journalposter.</value>
		public IDataObjectCollection<GeografiskObjektLink> SaksLenker
		{
			get { return _saksLenker ?? (_saksLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 3)); }
		}

		public IDataObjectCollection<GeografiskObjektLink> JournalPostLenker
		{
			get { return _journalpostLenker ?? (_journalpostLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 2)); }
		}
		
		public IDataObjectCollection<GeografiskObjektLink> SakspartLenker
		{
			get { return _sakspartLenker ?? (_sakspartLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 4)); }
		}
		
		public IDataObjectCollection<GeografiskObjektLink> AvsenderMottakerLenker
		{
			get { return _avsenderMottakerLenker ?? (_avsenderMottakerLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 1)); }
		}
		
		public IDataObjectCollection<GeografiskObjektLink> Lenker
		{
			get { return _lenker ?? (_lenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id)); }
		}
	}
}
