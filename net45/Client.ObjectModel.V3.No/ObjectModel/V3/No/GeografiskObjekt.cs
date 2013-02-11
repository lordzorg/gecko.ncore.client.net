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

        /// <summary>
        /// Gets the journal post lenker.
        /// </summary>
        /// <value>The journal post lenker.</value>
		public IDataObjectCollection<GeografiskObjektLink> JournalPostLenker
		{
			get { return _journalpostLenker ?? (_journalpostLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 2)); }
		}

        /// <summary>
        /// Gets the sakspart lenker.
        /// </summary>
        /// <value>The sakspart lenker.</value>
		public IDataObjectCollection<GeografiskObjektLink> SakspartLenker
		{
			get { return _sakspartLenker ?? (_sakspartLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 4)); }
		}

        /// <summary>
        /// Gets the avsender mottaker lenker.
        /// </summary>
        /// <value>The avsender mottaker lenker.</value>
		public IDataObjectCollection<GeografiskObjektLink> AvsenderMottakerLenker
		{
			get { return _avsenderMottakerLenker ?? (_avsenderMottakerLenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id && x.GeografiskEntitetsId == 1)); }
		}

        /// <summary>
        /// Gets the lenker.
        /// </summary>
        /// <value>The lenker.</value>
		public IDataObjectCollection<GeografiskObjektLink> Lenker
		{
			get { return _lenker ?? (_lenker = new TypedDataObjectCollection<GeografiskObjektLink>(x => x.GeografiskObjektId == Id)); }
		}
	}
}
