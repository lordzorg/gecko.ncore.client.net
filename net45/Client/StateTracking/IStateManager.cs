using System.Linq;

namespace Gecko.NCore.Client.StateTracking
{
	public interface IStateManager
	{
		/// <summary>
		/// Gets the entries.
		/// </summary>
		/// <value>The entries.</value>
		IQueryable<StateEntry> Entries { get; }

		/// <summary>
		/// Adds the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Add(object dataObject);

		/// <summary>
		/// Removes the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Remove(object dataObject);

		/// <summary>
		/// Attaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Attach(object dataObject);

		/// <summary>
		/// Detaches the specified data object.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void Detach(object dataObject);

		/// <summary>
		/// Attaches the specified data object with a weak reference.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		void WeakAttach(object dataObject);
	}
}