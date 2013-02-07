using System;
using System.Diagnostics;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
	[DebuggerDisplay("Name: {Name}, DataType: {DataType}")]
	public partial class CustomFieldDescriptor : ICustomFieldDescriptor
	{
		string ICustomFieldDescriptor.DataType
		{
			get { return DataType.ToString(); }
			set { DataType = (DataTypeEnum)Enum.Parse(typeof (DataTypeEnum), value, true); }
		}
	}
}
