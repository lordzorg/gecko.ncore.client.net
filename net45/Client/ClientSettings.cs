using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Gecko.NCore.Client.CookieManagement;

namespace Gecko.NCore.Client
{
    /// <summary>
    /// Class ClientSettings
    /// </summary>
	[DebuggerDisplay("Address: {Address}, Endpoint Name: {EndpointName}")]
	public class ClientSettings
	{
		/// <summary>
		/// Gets or sets the binding.
		/// </summary>
		/// <value>The service binding.</value>
		/// <remarks>Optional. If null, the default <see cref="Binding"/> in the configuration file is used</remarks>
		public Binding Binding { get; set; }

		/// <summary>
		/// Gets or sets the service address.
		/// </summary>
		/// <value>The service address.</value>
		/// <remarks>Optional. If null, the address in the given or default endpoint is overridden with this address.</remarks>
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the name of the service endpoint.
		/// </summary>
		/// <remarks>Optional. If null the default enpoint for current service contract is used.</remarks>
		/// <value>The name of the service endpoint.</value>
		public string EndpointName { get; set; }

        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
		public string Cookie { get; set; }
	}
}