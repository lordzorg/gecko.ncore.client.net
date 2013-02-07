using System.Collections.Generic;

namespace Gecko.NCore.Client.Functions
{
    /// <summary>
    /// Describes a function supported by the <see cref="FunctionManager"/>.
    /// </summary>
    public class FunctionDescriptor
    {
        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionDescriptor"/> class.
        /// </summary>
        public FunctionDescriptor()
        {
            
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IDictionary<string, string> Parameters
        {
            get { return _parameters; }
        }
    }
}