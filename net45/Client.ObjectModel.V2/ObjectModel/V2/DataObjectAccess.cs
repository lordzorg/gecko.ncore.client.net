namespace Gecko.NCore.Client.ObjectModel.V2
{
    internal class DataObjectAccess: DataObjectAccessBase
    {
        private readonly DataObject _requiredFlags;
        private readonly DataObject _readOnlyFlags;
        private readonly ObjectRights _objectRights;

        public DataObjectAccess(DataObject dataObject, DataObject requiredFlags, DataObject readOnlyFlags, ObjectRights objectRights)
			: base(dataObject)
        {
            _requiredFlags = requiredFlags;
            _readOnlyFlags = readOnlyFlags;
            _objectRights = objectRights;
        }

        /// <summary>
        /// Determines whether [is property required] [the specified property name].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// 	<c>true</c> if [is property required] [the specified property name]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsPropertyRequired(string propertyName)
        {
            var requiredPropertyFlagsType = _requiredFlags.GetType();
            var property = requiredPropertyFlagsType.GetProperty(propertyName);
            return (bool)property.GetValue(_requiredFlags, null);
        }

        /// <summary>
        /// Determines whether [is property read only] [the specified property name].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// 	<c>true</c> if [is property read only] [the specified property name]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsPropertyReadOnly(string propertyName)
        {
            var readOnlyPropertyFlagsType = _readOnlyFlags.GetType();
            var property = readOnlyPropertyFlagsType.GetProperty(propertyName);
            return !(bool)property.GetValue(_readOnlyFlags, null);
        }

        /// <summary>
        /// Gets a value indicating whether this instance can be modified.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can modified; otherwise, <c>false</c>.
        /// </value>
        public override bool CanModify
        {
            get { return _objectRights.KanEndre; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can be removed
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can be removed; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRemove
        {
            get { return _objectRights.KanSlette; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can be added.
        /// </summary>
        /// <value><c>true</c> if this instance can be added; otherwise, <c>false</c>.</value>
        public override bool CanAdd
        {
            get { return _objectRights.KanOpprette; }
        }
    }
}