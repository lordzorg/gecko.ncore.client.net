using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncFunctionsAdapter: IFunctionsAdapter
    {
        /// <summary>
        /// Executes the function with the specified name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        Task<object> ExecuteAsync(string functionName, params object[] arguments);

        /// <summary>
        /// Gets the supported functions.
        /// </summary>
        /// <value>The supported functions.</value>
        Task<IEnumerable<FunctionDescriptor>> GetSupportedFunctionsAsync();
    }
}
