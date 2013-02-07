using System.Collections.Generic;

namespace Gecko.NCore.Client.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFunctionsAdapter
    {
        /// <summary>
        /// Executes the function with the specified name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        object Execute(string functionName, params object[] arguments);

        /// <summary>
        /// Gets the supported functions.
        /// </summary>
        /// <value>The supported functions.</value>
        IEnumerable<FunctionDescriptor> SupportedFunctions { get; }
    }
}
