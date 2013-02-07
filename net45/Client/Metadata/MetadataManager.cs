using System.Collections.Generic;

namespace Gecko.NCore.Client.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class MetadataManager : IMetadataManager
    {
        private readonly IMetadataAdapter _metadataAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataManager"/> class.
        /// </summary>
        /// <param name="metadataAdapter">The documents adapter.</param>
        internal MetadataManager(IMetadataAdapter metadataAdapter)
        {
            _metadataAdapter = metadataAdapter;
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public IDictionary<string, string> GetMetadata(string objectType, object[] keys)
        {
            return _metadataAdapter.GetMetadata(objectType,keys);
        }
    }
}
