namespace Gecko.NCore.Client.ObjectModel
{
	public interface ICustomFieldDescriptor
	{
		string Caption { get; set; }

		string DataType { get; set; }

		string DefaultValue { get; set; }

		string Format { get; set; }

		string LegalValues { get; set; }

		int MaxLength { get; set; }

		string Name { get; set; }
	}
}