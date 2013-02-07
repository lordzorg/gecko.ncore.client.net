using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncMetadataAdapter: IMetadataAdapter
    {
        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        Task<IDictionary<string, string>> GetMetadataAsync(string objectType, object[] keys);
    }
}