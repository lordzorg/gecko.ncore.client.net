using System;
using System.ServiceModel;

namespace Gecko.NCore.Client
{
    public abstract class ServiceAdapterBase<TServiceClient>
        where TServiceClient: class, new()
    {
        private readonly ClientSettings _clientSettings;

        protected ServiceAdapterBase(ClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
        }

        /// <summary>
        /// Creates the object model service client.
        /// </summary>
        /// <typeparam name="TServiceClient">The type of the service client.</typeparam>
        /// <returns></returns>
        protected TServiceClient CreateServiceClient()
        {
            return CreateServiceClient(_clientSettings);
        }

        /// <summary>
        /// Creates the object model service client.
        /// </summary>
        /// <typeparam name="TServiceClient">The type of the service client.</typeparam>
        /// <param name="settings">The configuration.</param>
        /// <returns></returns>
        protected static TServiceClient CreateServiceClient(ClientSettings settings)
        {
            var instance = CreateObjectModelServiceClientInstance(settings);
            settings.AddCookieBeavior(instance);
            return instance;
        }

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
