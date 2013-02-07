using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gecko.NCore.Client.Functions.V2
{
	public class AsyncFunctionsAdapter: FunctionsAdapter, IAsyncFunctionsAdapter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionsAdapter"/> class.
		/// </summary>
		/// <param name="contextIdentity">The identity.</param>
		/// <param name="settings">The configuration.</param>
		public AsyncFunctionsAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(contextIdentity, settings)
		{
		}

        public async Task<object> ExecuteAsync(string functionName, params object[] arguments)
        {
            using (var functionsService = CreateServiceClient())
            {
                var result = await functionsService.ExecuteFunctionAsync(CreateEphorteIdentity(), functionName, arguments);
                return result.ResultValue;
            }
        }

        public async Task<IEnumerable<Functions.FunctionDescriptor>> GetSupportedFunctionsAsync()
        {
            using (var functionsService = CreateServiceClient())
            {
                var result = new List<Functions.FunctionDescriptor>();
                var ephorteIdentity = CreateEphorteIdentity();
                foreach (var functionDescriptor in await functionsService.QueryFunctionDescriptorsAsync(ephorteIdentity))
                {
                    var adaptedDescriptor = new Functions.FunctionDescriptor
                    {
                        Name = functionDescriptor.Name
                    };

                    foreach (var parameter in functionDescriptor.Parameters)
                    {
                        adaptedDescriptor.Parameters[parameter.Key] = parameter.Value.ToString();
                    }

                    result.Add(adaptedDescriptor);
                }
                return result;
            }
        }
    }
}
