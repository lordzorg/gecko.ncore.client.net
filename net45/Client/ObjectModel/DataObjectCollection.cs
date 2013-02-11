using System.Linq;
#if NET45
using System.Threading.Tasks;
#endif

namespace Gecko.NCore.Client.ObjectModel
{
    /// <summary>
    /// Class DataObjectCollection
    /// </summary>
	public abstract class DataObjectCollection: IDataObjectCollection
	{
		private bool _isLoaded;

		/// <summary>
		/// Gets a value indicating whether this instance is loaded.
		/// </summary>
		/// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
		public bool IsLoaded
		{
			get { return _isLoaded; }
		}

		/// <summary>
		/// Loads this collection.
		/// </summary>
		public void Load()
		{
			if (_isLoaded)
				return;

			LoadCore();

			_isLoaded = true;
		}

        /// <summary>
        /// Loads the core.
        /// </summary>
        protected abstract void LoadCore();

#if NET45
        /// <summary>
        /// Loads the async.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task LoadAsync()
        {
            if (_isLoaded)
                return;

            await LoadCoreAsync();

            _isLoaded = true;
        }

        /// <summary>
        /// Loads the core async.
        /// </summary>
        /// <returns>Task.</returns>
	    protected abstract Task LoadCoreAsync();
#endif

        /// <summary>
		/// Returns the relationship as a <see cref="IQueryable"/>.
		/// </summary>
		/// <returns></returns>
		public IQueryable AsQueryable()
		{
			return AsQueryableCore();
		}

        /// <summary>
        /// Ases the queryable core.
        /// </summary>
        /// <returns>IQueryable.</returns>
		protected abstract IQueryable AsQueryableCore();

        /// <summary>
        /// Gets or sets the current <see cref="IQueryProvider"/>.
        /// </summary>
	    public IQueryProvider QueryProvider { get; set; }
	}
}