using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Sak
    {
        private TypedDataObjectCollection<Journalpost> _journalposter;

        /// <summary>
        /// Gets the journalposter.
        /// </summary>
        /// <value>The journalposter.</value>
        public IDataObjectCollection<Journalpost> Journalposter
        {
            get { return _journalposter ?? (_journalposter = new TypedDataObjectCollection<Journalpost>(x => x.SakId == Id)); }
        }
    }
}
