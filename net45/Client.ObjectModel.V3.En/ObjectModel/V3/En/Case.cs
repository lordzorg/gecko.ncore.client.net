using System.Diagnostics;
using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	/// <summary>
	/// 
	/// </summary>
	[DebuggerDisplay("Id: {Id}, Reference: {CaseYear}/{SequenceNumber}")]
	public partial class Case
	{
		private TypedDataObjectCollection<RegistryEntry> _registryEntries;

		/// <summary>
		/// Gets the journalposter.
		/// </summary>
		/// <value>The journalposter.</value>
		public IDataObjectCollection<RegistryEntry> RegistryEntries
		{
			get { return _registryEntries ?? (_registryEntries = new TypedDataObjectCollection<RegistryEntry>(x => x.CaseId == Id)); }
		}

		private TypedDataObjectCollection<CaseLink> _links;

		/// <summary>
		/// Gets the links.
		/// </summary>
		/// <value>The links.</value>
		public IDataObjectCollection<CaseLink> Links
		{
			get { return _links ?? (_links = new TypedDataObjectCollection<CaseLink>(x => x.CaseId == Id)); }
		}

		private TypedDataObjectCollection<Classification> _classifications;

		/// <summary>
		/// Gets the classifications.
		/// </summary>
		/// <value>The classifications.</value>
		public IDataObjectCollection<Classification> Classifications
		{
			get { return _classifications ?? (_classifications = new TypedDataObjectCollection<Classification>(x => x.CaseId == Id)); }
		}

		private TypedDataObjectCollection<CaseParty> _caseParties;

		/// <summary>
		/// Gets the case parties.
		/// </summary>
		/// <value>The case parties.</value>
		public IDataObjectCollection<CaseParty> CaseParties
		{
			get { return _caseParties ?? (_caseParties = new TypedDataObjectCollection<CaseParty>(x => x.CaseId == Id)); }
		}
	}
}
