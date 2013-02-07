using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Gecko.NCore.Client.CookieManagement
{
	internal class CookieMessageInspector : IClientMessageInspector
	{
		private readonly string _cookie;

		public CookieMessageInspector(string cookie)
		{
			_cookie = cookie;
		}

		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
		}

		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			HttpRequestMessageProperty httpRequestMessage;
			object httpRequestMessageObject;
			if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
			{
				httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;

				if (httpRequestMessage != null && string.IsNullOrEmpty(httpRequestMessage.Headers["Cookie"]))
				{
					httpRequestMessage.Headers["Cookie"] = _cookie;
				}
			}
			else
			{
				httpRequestMessage = new HttpRequestMessageProperty();
				httpRequestMessage.Headers.Add("Cookie", _cookie);
				request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
			}

			return null;
		}

	}
}