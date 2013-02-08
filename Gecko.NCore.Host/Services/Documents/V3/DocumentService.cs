using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts.Documents.V3;

namespace Gecko.NCore.Host.Services.Documents.V3
{
	
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/documents/v3")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class DocumentService : IDocumentService
	{
		public UploadReturnMessage UploadFile(UploadMessage uploadMessage)
		{
			throw new NotImplementedException();
		}

		public void Checkin(CheckinMessage message)
		{
			throw new NotImplementedException();
		}

		public void CancelCheckout(CancelCheckoutRequest cancelCheckoutRequest)
		{
			throw new NotImplementedException();
		}

		public CheckoutResponseMessage Checkout(CheckoutRequest checkoutRequest)
		{
			throw new NotImplementedException();
		}

		public DocumentReturnMessage GetDocumentContent(GetDocumentContentMessage message)
		{
			throw new NotImplementedException();
		}

		public DocumentReturnMessage GetDocumentContentByJournalPostId(GetJournalpostDocumentContentMessage message)
		{
			throw new NotImplementedException();
		}

		public DocumentReturnMessage GetMoeteDocumentContent(GetMoeteDocumentContentMessage message)
		{
			throw new NotImplementedException();
		}

		public DocumentReturnMessage GetUtvalgDocumentContent(GetUtvalgDocumentContentMessage message)
		{
			throw new NotImplementedException();
		}
	}
}