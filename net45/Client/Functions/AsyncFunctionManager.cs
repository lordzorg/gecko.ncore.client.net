using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncFunctionManager : FunctionManager, IAsyncFunctionManager
    {
        private readonly IAsyncFunctionsAdapter _functionsAdapter;


        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncFunctionManager"/> class.
        /// </summary>
        internal AsyncFunctionManager(IAsyncFunctionsAdapter functionsAdapter)
            : base(functionsAdapter)
        {
            _functionsAdapter = functionsAdapter;
        }

        /// <summary>
        /// Executes the specified function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>Task{System.Object}.</returns>
        public async Task<object> ExecuteAsync(string functionName, params object[] arguments)
        {
            return await _functionsAdapter.ExecuteAsync(functionName, arguments);
        }

        /// <summary>
        /// Gets the supported functions.
        /// </summary>
        /// <returns>The supported functions.</returns>
        public async Task<IEnumerable<FunctionDescriptor>> GetSupportedFunctions()
        {
            return await _functionsAdapter.GetSupportedFunctionsAsync();
        }
    }
}
