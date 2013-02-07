using System.Collections.Generic;

namespace Gecko.NCore.Client.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMetadataManager
    {
        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        IDictionary<string, string> GetMetadata(string objectType, object[] keys);
    }
}