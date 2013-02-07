using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Querying;
using DynamicExpression = Gecko.NCore.Client.Querying.DynamicExpression;

namespace Gecko.NCore.Client
{
	public static class EphorteContextExtensions
	{
		public static TDataObject Create<TDataObject>(this IEphorteContext ephorteContext)
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
		public static IDataObjectAccess<TDataObject> Initialize<TDataObject>(this IEphorteContext ephorteContext, TDataObject dataObject) where TDataObject : class
		{
			var result = ephorteContext.Initialize(dataObject);
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
		public static IDataObjectAccess<TDataObject> Find<TDataObject>(this IEphorteContext ephorteContext, Expression<Func<TDataObject, bool>> predicate, params Expression<Func<TDataObject, object>>[] includeSelectors) where TDataObject : class
		{
			var relatedObjects = includeSelectors.Select(EvaluateMemberSelector).ToArray();
			var result = ephorteContext.Find(typeof(TDataObject).Name, ExtractPrimaryKeyFromKeySelector(predicate), relatedObjects);
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
		public static IDataObjectAccess Find(this IEphorteContext ephorteContext, string dataObjectName, string predicate, params string[] relatedObjects)
		{
			var dataObject = ephorteContext.Create(dataObjectName);
			var dataObjectType = dataObject.GetType();
			var predicateExpression = DynamicExpression.ParseLambda(dataObjectType, typeof(bool), predicate);
			var primaryKeys = ExtractPrimaryKeyFromKeySelector(predicateExpression);
			return ephorteContext.Find(dataObjectName, primaryKeys, relatedObjects);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptors<TDataObject>(this IEphorteContext ephorteContext, Expression<Func<TDataObject, bool>> predicate, string category)
		{
			return ephorteContext.GetCustomFieldDescriptors(typeof(TDataObject).Name, ExtractPrimaryKeyFromKeySelector(predicate), category);
		}

		/// <summary>
		/// Gets the custom field descriptors.
		/// </summary>
		/// <param name="ephorteContext">The ephorte context.</param>
		/// <param name="dataObjectName">Name of the data object.</param>
		/// <param name="predicate">The predicate.</param>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public static ICollection<ICustomFieldDescriptor> GetCustomFieldDescriptors(this IEphorteContext ephorteContext, string dataObjectName, string predicate, string category)
		{
			var dataObject = ephorteContext.Create(dataObjectName);
			var dataObjectType = dataObject.GetType();
			var predicateExpression = DynamicExpression.ParseLambda(dataObjectType, typeof(bool), predicate);
			var primaryKeys = ExtractPrimaryKeyFromKeySelector(predicateExpression);
			return ephorteContext.GetCustomFieldDescriptors(dataObjectName, primaryKeys, category);
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
