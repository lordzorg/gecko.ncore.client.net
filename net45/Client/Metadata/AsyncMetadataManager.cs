using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncMetadataManager : MetadataManager, IAsyncMetadataManager
    {
        private readonly IAsyncMetadataAdapter _metadataAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataManager"/> class.
        /// </summary>
        /// <param name="metadataAdapter">The documents adapter.</param>
        internal AsyncMetadataManager(IAsyncMetadataAdapter metadataAdapter)
            : base(metadataAdapter)
        {
            _metadataAdapter = metadataAdapter;
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public async Task<IDictionary<string, string>> GetMetadataAsync(string objectType, object[] keys)
        {
            return await _metadataAdapter.GetMetadataAsync(objectType, keys);
        }
    }
}
