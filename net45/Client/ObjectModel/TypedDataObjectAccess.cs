using System;
using System.Linq.Expressions;
using Gecko.NCore.Client.Querying;

namespace Gecko.NCore.Client.ObjectModel
{
	internal class TypedDataObjectAccess<TDataObject>: IDataObjectAccess<TDataObject>
	{
		private readonly IDataObjectAccess _innerDataObjectAccess;

		internal TypedDataObjectAccess(IDataObjectAccess innerDataObjectAccess)
		{
			_innerDataObjectAccess = innerDataObjectAccess;
		}

		public TDataObject DataObject
		{
			get { return (TDataObject) _innerDataObjectAccess.DataObject; }
		}

		public TDataObject RequiredFlags
		{
			get { return (TDataObject) _innerDataObjectAccess.RequiredFlags;  }
		}

		public TDataObject ReadOnlyFlags
		{
			get { return (TDataObject) _innerDataObjectAccess.ReadOnlyFlags; }
		}

		/// <summary>
		/// Determines whether the specified property selector is required.
		/// </summary>
		/// <param name="propertySelector">The property selector.</param>
		/// <returns>
		/// 	<c>true</c> if the specified property selector is required; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPropertyRequired(Expression<Func<TDataObject, object>> propertySelector)
		{
			return _innerDataObjectAccess.IsPropertyRequired(MemberEvaluator.Evaluate(propertySelector));
		}

		/// <summary>
		/// Determines whether [is read only] [the specified property selector].
		/// </summary>
		/// <param name="propertySelector">The property selector.</param>
		/// <returns>
		/// 	<c>true</c> if [is read only] [the specified property selector]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPropertyReadOnly(Expression<Func<TDataObject, object>> propertySelector)
		{
			return _innerDataObjectAccess.IsPropertyReadOnly(MemberEvaluator.Evaluate(propertySelector));
		}

		public bool CanModify
		{
			get { return _innerDataObjectAccess.CanModify; }
		}

		public bool CanRemove
		{
			get { return _innerDataObjectAccess.CanRemove; }
		}

		public bool CanAdd
		{
			get { return _innerDataObjectAccess.CanAdd; }
		}
	}
}
