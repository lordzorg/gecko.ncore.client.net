using System.Diagnostics;
using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel.V3.En
{
	/// <summary>
	/// 
	/// </summary>
	[DebuggerDisplay("Id: {Id}")]
	public partial class DocumentDescription
	{
		private TypedDataObjectCollection<DocumentObject> _documentObjects;

		/// <summary>
		/// Gets the document objects.
		/// </summary>
		/// <value>The document objects.</value>
		public IDataObjectCollection<DocumentObject> DocumentObjects
		{
			get { return _documentObjects ?? (_documentObjects = new TypedDataObjectCollection<DocumentObject>(x => x.DocumentDescriptionId == Id)); }
		}

		private TypedDataObjectCollection<RegistryEntryDocument> _registryEntryDocuments;

		/// <summary>
		/// Gets the registry entry documents.
		/// </summary>
		/// <value>The registry entry documents.</value>
		public IDataObjectCollection<RegistryEntryDocument> RegistryEntryDocuments
		{
			get { return _registryEntryDocuments ?? (_registryEntryDocuments = new TypedDataObjectCollection<RegistryEntryDocument>(x => x.DocumentDescriptionId == Id)); }
		}
	}
}
