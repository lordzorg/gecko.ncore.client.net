using System.Collections.Generic;
using System.ServiceModel;

namespace Gecko.NCore.Client.Functions.V2
{
	public class FunctionsAdapter: ServiceAdapterBase<FunctionsServiceClient>, IFunctionsAdapter
	{
		private readonly EphorteContextIdentity _contextIdentity;

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionsAdapter"/> class.
		/// </summary>
		/// <param name="contextIdentity">The identity.</param>
		/// <param name="settings">The configuration.</param>
		public FunctionsAdapter(EphorteContextIdentity contextIdentity, ClientSettings settings)
            : base(settings)
		{
			_contextIdentity = contextIdentity;
		}

		protected EphorteIdentity CreateEphorteIdentity()
		{
			return new EphorteIdentity
					   {
						   UserName = _contextIdentity.Username, 
						   Password = _contextIdentity.Password,
						   Role = _contextIdentity.Role,
						   Database = _contextIdentity.Database,
						   ExternalSystemName = _contextIdentity.ExternalSystemName
					   };
		}

	    /// <summary>
		/// Executes the function with the specified name.
		/// </summary>
		/// <param name="functionName">Name of the function.</param>
		/// <param name="arguments">The arguments.</param>
		/// <returns></returns>
		public object Execute(string functionName, params object[] arguments)
		{
			using (var functionsService = CreateServiceClient())
			{
				var result = functionsService.ExecuteFunction(CreateEphorteIdentity(), functionName, arguments);
				return result.ResultValue;
			}
		}

		/// <summary>
		/// Gets the supported functions.
		/// </summary>
		/// <value>The supported functions.</value>
		public IEnumerable<Functions.FunctionDescriptor> SupportedFunctions
		{
			get
			{
				using (var functionsService = CreateServiceClient())
				{
					var ephorteIdentity = CreateEphorteIdentity();
					foreach (var functionDescriptor in functionsService.QueryFunctionDescriptors(ephorteIdentity))
					{
						var adaptedDescriptor = new Functions.FunctionDescriptor
													{
														Name = functionDescriptor.Name
													};

						foreach (var parameter in functionDescriptor.Parameters)
						{
							adaptedDescriptor.Parameters[parameter.Key] = parameter.Value.ToString();
						}
						yield return adaptedDescriptor;
					}
				}
			}
		}
    }
}
