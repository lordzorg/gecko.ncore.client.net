using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Querying;
using DynamicExpression = Gecko.NCore.Client.Querying.DynamicExpression;

namespace Gecko.NCore.Client
{
	public static class AsyncEphorteContextExtensions
	{
		public static TDataObject Create<TDataObject>(this IAsyncEphorteContext ephorteContext)
		{
			return (TDataObject) ephorteContext.Create(typeof (TDataObject).Name);
		}

        /// <summary>
        /// Initializes the specified ephorte context.
        /// </summary>
        /// <typeparam name="TDataObject">The type of the data object.</typeparam>
        /// <param name="ephorteContext">The ephorte context.</param>
        /// <param name="dataObject">The data object.</param>
        /// <returns></returns>
        public static async Task<IDataObjectAccess<TDataObject>> InitializeAsync<TDataObject>(this IAsyncEphorteContext ephorteContext, TDataObject dataObject) where TDataObject : class
        {
            var result = await ephorteContext.InitializeAsync(dataObject);
            return new TypedDataObjectAccess<TDataObject>(result);
        }

        /// <summary>
        /// Finds the specified key selector.
        /// </summary>
        /// <typeparam name="TDataObject">The type of the data object.</typeparam>
        /// <param name="ephorteContext">The ephorte context.</param>
        /// <param name="predicate">The key selector.</param>
        /// <param name="includeSelectors">The include selectors.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static async Task<IDataObjectAccess<TDataObject>> FindAsync<TDataObject>(this IAsyncEphorteContext ephorteContext, Expression<Func<TDataObject, bool>> predicate, params Expression<Func<TDataObject, object>>[] includeSelectors) where TDataObject : class
        {
            var relatedObjects = includeSelectors.Select(EvaluateMemberSelector).ToArray();
            var result = await ephorteContext.FindAsync(typeof(TDataObject).Name, ExtractPrimaryKeyFromKeySelector(predicate), relatedObjects);
            return new TypedDataObjectAccess<TDataObject>(result);
        }

        /// <summary>
        /// Finds the specified ephorte context.
        /// </summary>
        /// <param name="ephorteContext">The ephorte context.</param>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="relatedObjects">The related objects.</param>
        /// <returns></returns>
        public static async Task<IDataObjectAccess> FindAsync(this IAsyncEphorteContext ephorteContext, string dataObjectName, string predicate, params string[] relatedObjects)
        {
            var dataObject = ephorteContext.Create(dataObjectName);
            var dataObjectType = dataObject.GetType();
            var predicateExpression = DynamicExpression.ParseLambda(dataObjectType, typeof(bool), predicate);
            var primaryKeys = ExtractPrimaryKeyFromKeySelector(predicateExpression);
            return await ephorteContext.FindAsync(dataObjectName, primaryKeys, relatedObjects);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static async Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorsAsync<TDataObject>(this IAsyncEphorteContext ephorteContext, Expression<Func<TDataObject, bool>> predicate, string category)
        {
            return await ephorteContext.GetCustomFieldDescriptorsAsync(typeof(TDataObject).Name, ExtractPrimaryKeyFromKeySelector(predicate), category);
        }

        /// <summary>
        /// Gets the custom field descriptors.
        /// </summary>
        /// <param name="ephorteContext">The ephorte context.</param>
        /// <param name="dataObjectName">Name of the data object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<ICollection<ICustomFieldDescriptor>> GetCustomFieldDescriptorsAsync(this IAsyncEphorteContext ephorteContext, string dataObjectName, string predicate, string category)
        {
            var dataObject = ephorteContext.Create(dataObjectName);
            var dataObjectType = dataObject.GetType();
            var predicateExpression = DynamicExpression.ParseLambda(dataObjectType, typeof(bool), predicate);
            var primaryKeys = ExtractPrimaryKeyFromKeySelector(predicateExpression);
            return await ephorteContext.GetCustomFieldDescriptorsAsync(dataObjectName, primaryKeys, category);
        }
        
        private static IDictionary<string, string> ExtractPrimaryKeyFromKeySelector(Expression predicate)
		{
			return PrimaryKeyEvaluator.Evaluate(predicate);
		}

		private static string EvaluateMemberSelector<TDataObject>(Expression<Func<TDataObject, object>> memberSelector)
		{
			return MemberEvaluator.Evaluate(memberSelector);
		}
	}
}
