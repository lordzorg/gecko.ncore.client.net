using System.Collections.Generic;
using System.Linq;
#if NET45
using System.Threading.Tasks;
#endif

namespace Gecko.NCore.Client.ObjectModel
{
	/// <summary>
	/// Provides support for lazy loading data object collections.
	/// </summary>
	public interface IDataObjectCollection
	{
		/// <summary>
		/// Loads this collection.
		/// </summary>
		void Load();

#if NET45
        /// <summary>
        /// Loads the async.
        /// </summary>
        /// <returns>Task.</returns>
	    Task LoadAsync();
#endif

		/// <summary>
		/// Gets a value indicating whether this instance is loaded.
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		bool IsLoaded { get; }

		/// <summary>
		/// Returns the relationship as a <see cref="IQueryable"/>.
		/// </summary>
		/// <returns></returns>
		IQueryable AsQueryable();

        /// <summary>
        /// Gets or sets the current <see cref="IQueryProvider"/>.
        /// </summary>
        IQueryProvider QueryProvider { get; set; }
	}
	
	/// <summary>
	/// Provides support for lazy loading data object collections.
	/// </summary>
	/// <typeparam name="TDataObject">The type of the data object.</typeparam>
#if NET45
	public interface IDataObjectCollection<out TDataObject>: IDataObjectCollection, IReadOnlyCollection<TDataObject>
#else 
	public interface IDataObjectCollection<TDataObject>: IDataObjectCollection, ICollection<TDataObject>
#endif
    {
		/// <summary>
		/// Returns the relationship as a <see cref="IQueryable{TDataObject}"/>.
		/// </summary>
		/// <returns></returns>
		new IQueryable<TDataObject> AsQueryable();
	}
}