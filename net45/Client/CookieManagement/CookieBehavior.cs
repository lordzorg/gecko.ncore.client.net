using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Gecko.NCore.Client.CookieManagement
{
	internal class CookieBehavior : IEndpointBehavior
	{
		private readonly string _cookie;

		public CookieBehavior(string cookie)
		{
			_cookie = cookie;
		}

		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, 
		                                 BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint,
		                                ClientRuntime behavior)
		{
			behavior.MessageInspectors.Add(new CookieMessageInspector(_cookie));
		}

		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint,
		                                  EndpointDispatcher endpointDispatcher)
		{
		}

		public void Validate(ServiceEndpoint serviceEndpoint)
		{
		}
	}
}