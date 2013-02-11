using System;
using System.ServiceModel;

namespace Gecko.NCore.Client
{
    /// <summary>
    /// Class ServiceAdapterBase
    /// </summary>
    /// <typeparam name="TServiceClient">The type of the T service client.</typeparam>
    public abstract class ServiceAdapterBase<TServiceClient>
        where TServiceClient: class, new()
    {
        private readonly ClientSettings _clientSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAdapterBase{TServiceClient}" /> class.
        /// </summary>
        /// <param name="clientSettings">The client settings.</param>
        protected ServiceAdapterBase(ClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
        }

        /// <summary>
        /// Creates the object model service client.
        /// </summary>
        /// <returns>`0.</returns>
        protected TServiceClient CreateServiceClient()
        {
            return CreateServiceClient(_clientSettings);
        }

        /// <summary>
        /// Creates the object model service client.
        /// </summary>
        /// <param name="settings">The configuration.</param>
        /// <returns>`0.</returns>
        protected static TServiceClient CreateServiceClient(ClientSettings settings)
        {
            var instance = CreateObjectModelServiceClientInstance(settings);
            settings.AddCookieBeavior(instance);
            return instance;
        }

        /// <summary>
        /// Creates the object model service client instance.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>`0.</returns>
        protected static TServiceClient CreateObjectModelServiceClientInstance(ClientSettings settings)
        {
            if (settings.Binding != null && !string.IsNullOrEmpty(settings.Address))
                return (TServiceClient)Activator.CreateInstance(typeof(TServiceClient), settings.Binding, new EndpointAddress(settings.Address));

            if (!string.IsNullOrEmpty(settings.EndpointName) && !string.IsNullOrEmpty(settings.Address))
                return (TServiceClient)Activator.CreateInstance(typeof(TServiceClient), settings.EndpointName, settings.Address);

            if (!string.IsNullOrEmpty(settings.EndpointName))
                return (TServiceClient)Activator.CreateInstance(typeof(TServiceClient), settings.EndpointName);

            return new TServiceClient();
        }
    }
}
