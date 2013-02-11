using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ephorte.ServiceModel.Contracts.Notification.V1;


namespace Gecko.NCore.Host.Services.Notification.V1
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/notification/v1")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class NotificationService : INotificationService
	{
		public void Notify(EphorteIdentity ephorteIdentity, NotificationRequest notificationRequest)
		{
			throw new NotImplementedException();
		}
	}
}