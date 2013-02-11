using System;
using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// Class DirectPredicate
    /// </summary>
    /// <typeparam name="TDataObject">The type of the T data object.</typeparam>
	public static class DirectPredicate<TDataObject>
	{
        /// <summary>
        /// Members the specified property selector.
        /// </summary>
        /// <typeparam name="TProperty">The type of the T property.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>``0.</returns>
        /// <exception cref="System.ArgumentNullException">propertySelector</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static TProperty Member<TProperty>(Expression<Func<TDataObject, TProperty>> propertySelector)
		{
			if (propertySelector == null)
				throw new ArgumentNullException("propertySelector");

			return default(TProperty);
		}
	}
}
