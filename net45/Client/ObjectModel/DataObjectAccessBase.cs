namespace Gecko.NCore.Client.ObjectModel
{
    public abstract class DataObjectAccessBase: IDataObjectAccess
    {
    	private readonly object _dataObject;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataObjectAccessBase"/> class.
		/// </summary>
		/// <param name="dataObject">The data object.</param>
		protected DataObjectAccessBase(object dataObject)
		{
			_dataObject = dataObject;
		}

    	/// <summary>
		/// Gets the data object.
		/// </summary>
		/// <value>The data object.</value>
		public object DataObject
    	{
    		get { return _dataObject; }
    	}

    	/// <summary>
        /// Determines whether [is property required] [the specified property name].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// 	<c>true</c> if [is property required] [the specified property name]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsPropertyRequired(string propertyName);

        /// <summary>
        /// Determines whether [is property read only] [the specified property name].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// 	<c>true</c> if [is property read only] [the specified property name]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsPropertyReadOnly(string propertyName);

        /// <summary>
        /// Gets a value indicating whether this instance can be modified.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can modified; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CanModify { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can be removed
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can be removed; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CanRemove { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can be added.
        /// </summary>
        /// <value><c>true</c> if this instance can be added; otherwise, <c>false</c>.</value>
        public abstract bool CanAdd { get; }
    }
}
