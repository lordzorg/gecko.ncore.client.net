namespace Gecko.NCore.Client.ObjectModel.V3.No
{
	internal class DataObjectAccess: DataObjectAccessBase
	{
		private readonly ObjectRights _objectRights;

		public DataObjectAccess(DataObject dataObject, DataObject requiredFlags, DataObject readOnlyFlags, ObjectRights objectRights)
			: base(dataObject, requiredFlags, readOnlyFlags)
		{
			_objectRights = objectRights;
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