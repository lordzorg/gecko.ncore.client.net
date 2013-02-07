using System.Collections.Generic;

namespace Gecko.NCore.Client.Metadata.V1
{
	public class MetadataAdapter : ServiceAdapterBase<MetadataServiceClient>, IMetadataAdapter
	{
		private readonly EphorteContextIdentity _contextIdentity;

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataAdapter"/> class.
		/// </summary>
		/// <param name="contextIdentity">The identity.</param>
		/// <param name="settings">The configuration.</param>
		public MetadataAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(settings)
		{
			_contextIdentity = contextIdentity;
		}

		protected EphorteIdentity CreateEphorteIdentity()
		{
			return new EphorteIdentity
					   {
						   UserName = _contextIdentity.Username, 
						   Password = _contextIdentity.Password,
						   Role = _contextIdentity.Role,
						   Database = _contextIdentity.Database,
						   ExternalSystemName = _contextIdentity.ExternalSystemName
					   };
		}

	    /// <summary>
		/// Gets the metadata.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <returns></returns>
		public IDictionary<string, string> GetMetadata(string objectType, object[] keys)
		{
		    using (var metadataService = CreateServiceClient())
			{
				var result = metadataService.GetMetadata(CreateEphorteIdentity(),
														 new MetadataIdentifier {Keys = keys, ObjectType = objectType});

				return result;
			}
		}
	}
}
