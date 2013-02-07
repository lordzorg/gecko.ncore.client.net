using System.Collections.Generic;

namespace Gecko.NCore.Client.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionManager : IFunctionManager
    {
        private readonly IFunctionsAdapter _functionsAdapter;


        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionManager"/> class.
        /// </summary>
        internal FunctionManager(IFunctionsAdapter functionsAdapter)
        {
            _functionsAdapter = functionsAdapter;
        }

        /// <summary>
        /// Executes the specified function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public object Execute(string functionName, params object[] arguments)
        {
            return _functionsAdapter.Execute(functionName, arguments);
        }

        /// <summary>
        /// Gets the supported functions.
        /// </summary>
        /// <value>The supported functions.</value>
        public IEnumerable<FunctionDescriptor> SupportedFunctions
        {
            get { return _functionsAdapter.SupportedFunctions; }
        }
    }
}
