using System.ServiceModel.Description;
using Gecko.NCore.Client.CookieManagement;

namespace Gecko.NCore.Client
{
	public static class ClientSettingsExtensions
	{
		public static void AddCookieBeavior(this ClientSettings settings, object serviceClient)
		{
			if (serviceClient == null || string.IsNullOrEmpty(settings.Cookie)) 
				return;
			
			var type = serviceClient.GetType();
			var endpointProperty = type.GetProperty("Endpoint");
			if (endpointProperty == null) 
				return;
			
			var endpoint = endpointProperty.GetValue(serviceClient, null) as ServiceEndpoint;
			if (endpoint != null)
			{
				endpoint.Behaviors.Add(new CookieBehavior(settings.Cookie));
			}
		}
	}
}
