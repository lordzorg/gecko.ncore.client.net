using System;
using System.Linq.Expressions;

namespace Gecko.NCore.Client.ObjectModel
{
    /// <summary>
    /// Interface IDataObjectAccess
    /// </summary>
	public interface IDataObjectAccess
	{
        /// <summary>
        /// Gets the data object.
        /// </summary>
        /// <value>The data object.</value>
		object DataObject { get; }

		/// <summary>
		/// Determines whether [is property required] [the specified property name].
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>
		/// 	<c>true</c> if [is property required] [the specified property name]; otherwise, <c>false</c>.
		/// </returns>
		bool IsPropertyRequired(string propertyName);

		/// <summary>
		/// Determines whether [is property read only] [the specified property name].
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>
		/// 	<c>true</c> if [is property read only] [the specified property name]; otherwise, <c>false</c>.
		/// </returns>
		bool IsPropertyReadOnly(string propertyName);

		/// <summary>
		/// Gets a value indicating whether this instance can be modified.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can modified; otherwise, <c>false</c>.
		/// </value>
		bool CanModify { get; }

		/// <summary>
		/// Gets a value indicating whether this instance can be removed
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can be removed; otherwise, <c>false</c>.
		/// </value>
		bool CanRemove { get; }

		/// <summary>
		/// Gets a value indicating whether this instance can be added.
		/// </summary>
		/// <value><c>true</c> if this instance can be added; otherwise, <c>false</c>.</value>
		bool CanAdd { get; }
	}

    /// <summary>
    /// Interface IDataObjectAccess
    /// </summary>
    /// <typeparam name="TDataObject">The type of the T data object.</typeparam>
	public interface IDataObjectAccess<TDataObject>
	{
		/// <summary>
		/// Gets the data object.
		/// </summary>
		/// <value>The data object.</value>
		TDataObject DataObject { get; }

		/// <summary>
		/// Determines whether the specified property selector is required.
		/// </summary>
		/// <param name="propertySelector">The property selector.</param>
		/// <returns>
		/// 	<c>true</c> if the specified property selector is required; otherwise, <c>false</c>.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		bool IsPropertyRequired(Expression<Func<TDataObject, object>> propertySelector);

		/// <summary>
		/// Determines whether [is read only] [the specified property selector].
		/// </summary>
		/// <param name="propertySelector">The property selector.</param>
		/// <returns>
		/// 	<c>true</c> if [is read only] [the specified property selector]; otherwise, <c>false</c>.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		bool IsPropertyReadOnly(Expression<Func<TDataObject, object>> propertySelector);

		/// <summary>
		/// Gets a value indicating whether this instance can be modified.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can modified; otherwise, <c>false</c>.
		/// </value>
		bool CanModify { get; }

		/// <summary>
		/// Gets a value indicating whether this instance can be removed
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can be removed; otherwise, <c>false</c>.
		/// </value>
		bool CanRemove { get; }

		/// <summary>
		/// Gets a value indicating whether this instance can be added.
		/// </summary>
		/// <value><c>true</c> if this instance can be added; otherwise, <c>false</c>.</value>
		bool CanAdd { get; }
	}
}