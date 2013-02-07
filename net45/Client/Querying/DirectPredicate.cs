using System;
using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
	public static class DirectPredicate<TDataObject>
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static TProperty Member<TProperty>(Expression<Func<TDataObject, TProperty>> propertySelector)
		{
			if (propertySelector == null)
				throw new ArgumentNullException("propertySelector");

			return default(TProperty);
		}
	}
}
