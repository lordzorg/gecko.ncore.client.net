using System;
using System.ServiceModel;
using Ephorte.ServiceModel.Contracts.Notification.V1;


namespace Gecko.NCore.Host.Services.Notification.V1
{
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://www.gecko.no/ephorte/services/notification/v1")]
	public class NotificationService : INotificationService
	{
		public void Notify(EphorteIdentity ephorteIdentity, NotificationRequest notificationRequest)
		{
			throw new NotImplementedException();
		}
	}
}