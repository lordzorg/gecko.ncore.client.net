using System.ServiceModel.Description;
using Gecko.NCore.Client.CookieManagement;

namespace Gecko.NCore.Client
{
    /// <summary>
    /// Class ClientSettingsExtensions
    /// </summary>
	public static class ClientSettingsExtensions
	{
        /// <summary>
        /// Adds the cookie beavior.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="serviceClient">The service client.</param>
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
