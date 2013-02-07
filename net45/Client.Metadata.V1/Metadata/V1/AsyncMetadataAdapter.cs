using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Metadata.V1
{
	public class AsyncMetadataAdapter : MetadataAdapter, IAsyncMetadataAdapter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataAdapter"/> class.
		/// </summary>
		/// <param name="contextIdentity">The identity.</param>
		/// <param name="settings">The configuration.</param>
		public AsyncMetadataAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(contextIdentity, settings)
		{
		}

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public async Task<IDictionary<string, string>> GetMetadataAsync(string objectType, object[] keys)
        {
            using (var metadataService = CreateServiceClient())
            {
                var result = await metadataService.GetMetadataAsync(CreateEphorteIdentity(), new MetadataIdentifier { Keys = keys, ObjectType = objectType });

                return result;
            }
        }
    }
}
