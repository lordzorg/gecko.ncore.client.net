using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncFunctionManager: IFunctionManager
    {
        /// <summary>
        /// Executes the specified function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        Task<object> ExecuteAsync(string functionName, params object[] arguments);

        /// <summary>
        /// Gets the supported functions.
        /// </summary>
        /// <returns>The supported functions.</returns>
        Task<IEnumerable<FunctionDescriptor>>  GetSupportedFunctions();
    }
}