using System;
using System.Collections.Generic;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// Provides access to context specific values. Such as the current user.
	/// </summary>
	public class QueryContext
	{
		private readonly static QueryContext Instance = new QueryContext();

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryContext"/> class.
		/// </summary>
		private QueryContext()
		{
		}

		/// <summary>
		/// Gets the current <see cref="QueryContext"/>.
		/// </summary>
		/// <value>The current.</value>
		public static QueryContext Current
		{
			get { return Instance; }
		}

		/// <summary>
		/// Gets the active user id.
		/// </summary>
		/// <value>The active user id.</value>
		public int ActiveUserId
		{
			get { return ActiveUserIdCore; }
		}

		/// <summary>
		/// Gets the active user name id.
		/// </summary>
		/// <value>The active user name id.</value>
		public int ActiveUserNameId
		{
			get { return ActiveUserNameIdCore; }
		}

		/// <summary>
		/// Gets the administrative unit id.
		/// </summary>
		/// <value>The administrative unit id.</value>
		public int ActiveAdministrativeUnitId
		{
			get { return ActiveAdministrativeUnitIdCore; }
		}

		/// <summary>
		/// Gets the active record unit id.
		/// </summary>
		/// <value>The record unit id.</value>
		public string ActiveRegistryManagementUnitId
		{
			get { return ActiveRegistryManagementUnitIdCore; }
		}

		/// <summary>
		/// Determines whether the current user has write access to the administrative unit based on the specified administrative unit id.
		/// </summary>
		/// <value>The writeable administrative units.</value>
		public IEnumerable<int> WritableAdministrativeUnitIds
		{
			get { return WritableAdministrativeUnitIdsCore; }
		}

		/// <summary>
		/// Determines whether the specified administrative unit id is in administrative unit sub hierarchy.
		/// </summary>
		/// <value>The active administrative unit hierarchy ids.</value>
		public IEnumerable<int> ActiveAdministrativeUnitHierarchyIds
		{
			get { return ActiveAdministrativeUnitHierarchyIdsCore; }
		}

		/// <summary>
		/// Determines whether the specified administrative unit id is in administrative unit hierarchy.
		/// </summary>
		/// <value>The active administrative unit sub hierarchy ids.</value>
		public IEnumerable<int> ActiveAdministrativeUnitSubHierarchyIds
		{
			get { return ActiveAdministrativeUnitSubHierarchyIdsCore; }
		}

		/// <summary>
		/// Determines where the specified Classification System Id is the active Classification System Id			
		/// </summary>
		public string ActiveClassificationSystemId
		{
			get { return ActiveClassificationSystemIdCore; }	
		}

		/// <summary>
		/// Determines if th specified Municipality Id is the Active Municipality Id.
		/// </summary>
		public string ActiveMunicipalityId
		{
			get { return ActiveMunicipalityIdCore; } 
		}

		/// <summary>
		/// Storeds the query.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="queryId">The query id.</param>
		/// <returns></returns>
		public bool StoredQuery<TModel>(TModel model, int queryId)
		{
			return StoredQueryCore(model, queryId);
		}

        /// <summary>
        /// Gets the active user id core.
        /// </summary>
        /// <value>The active user id core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual int ActiveUserIdCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active user name id core.
        /// </summary>
        /// <value>The active user name id core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual int ActiveUserNameIdCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active administrative unit id core.
        /// </summary>
        /// <value>The active administrative unit id core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual int ActiveAdministrativeUnitIdCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active classification system id core.
        /// </summary>
        /// <value>The active classification system id core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual string ActiveClassificationSystemIdCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active registry management unit id core.
        /// </summary>
        /// <value>The active registry management unit id core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual string ActiveRegistryManagementUnitIdCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active municipality id core.
        /// </summary>
        /// <value>The active municipality id core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual string ActiveMunicipalityIdCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the writable administrative unit ids core.
        /// </summary>
        /// <value>The writable administrative unit ids core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual IEnumerable<int> WritableAdministrativeUnitIdsCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active administrative unit hierarchy ids core.
        /// </summary>
        /// <value>The active administrative unit hierarchy ids core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual IEnumerable<int> ActiveAdministrativeUnitHierarchyIdsCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Gets the active administrative unit sub hierarchy ids core.
        /// </summary>
        /// <value>The active administrative unit sub hierarchy ids core.</value>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual IEnumerable<int> ActiveAdministrativeUnitSubHierarchyIdsCore
		{
			get
			{
				throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
			}
		}

        /// <summary>
        /// Storeds the query core.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="queryId">The query id.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        /// <exception cref="System.NotSupportedException"></exception>
		protected virtual bool StoredQueryCore<TModel>(TModel model, int queryId)
		{
			throw new NotSupportedException(Resources.QueryContext_Properties_on_QueryContext_Current_are_only_supported_in_a_Linq_query_based_on_IEphorteContext);
		}
	}
}
