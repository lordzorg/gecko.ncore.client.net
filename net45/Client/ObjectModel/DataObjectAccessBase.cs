namespace Gecko.NCore.Client.ObjectModel
{
    /// <summary>
    /// Class DataObjectAccessBase
    /// </summary>
    public abstract class DataObjectAccessBase: IDataObjectAccess
    {
	    /// <summary>
	    /// Initializes a new instance of the <see cref="DataObjectAccessBase"/> class.
	    /// </summary>
	    /// <param name="dataObject">The data object.</param>
	    /// <param name="requiredFlags">Required flags</param>
	    /// <param name="readOnlyFlags">ReadOnly flags</param>
	    protected DataObjectAccessBase(object dataObject, object requiredFlags, object readOnlyFlags)
		{
			DataObject = dataObject;
			RequiredFlags = requiredFlags;
		    ReadOnlyFlags = readOnlyFlags;
		}

	    /// <summary>
		/// Gets the data object.
		/// </summary>
		/// <value>The data object.</value>
		public object DataObject { get; private set; }

	    /// <summary>
	    /// Returns an object with all the fields of the DataObject. Each field return a boolean indicating if the field is required.
	    /// </summary>
		public object RequiredFlags { get; private set; }

	    /// <summary>
	    /// Returns an object with all the fields of the DataObject. Each field return a boolean indicating if the field is read only.
	    /// </summary>
	    public object ReadOnlyFlags { get; private set; }

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

	    /// <summary>
	    /// Determines whether [is property required] [the specified property name].
	    /// </summary>
	    /// <param name="propertyName">Name of the property.</param>
	    /// <returns>
	    /// 	<c>true</c> if [is property required] [the specified property name]; otherwise, <c>false</c>.
	    /// </returns>
	    public bool IsPropertyRequired(string propertyName)
	    {
		    var requiredPropertyFlagsType = RequiredFlags.GetType();
		    var property = requiredPropertyFlagsType.GetProperty(propertyName);
		    return (bool)property.GetValue(RequiredFlags, null);
	    }

	    /// <summary>
	    /// Determines whether [is property read only] [the specified property name].
	    /// </summary>
	    /// <param name="propertyName">Name of the property.</param>
	    /// <returns>
	    /// 	<c>true</c> if [is property read only] [the specified property name]; otherwise, <c>false</c>.
	    /// </returns>
	    public bool IsPropertyReadOnly(string propertyName)
	    {
		    var readOnlyPropertyFlagsType = ReadOnlyFlags.GetType();
		    var property = readOnlyPropertyFlagsType.GetProperty(propertyName);
		    return !(bool)property.GetValue(ReadOnlyFlags, null);
	    }
    }
}
