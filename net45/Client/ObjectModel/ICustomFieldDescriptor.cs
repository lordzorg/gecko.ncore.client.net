namespace Gecko.NCore.Client.ObjectModel
{
    /// <summary>
    /// Interface ICustomFieldDescriptor
    /// </summary>
	public interface ICustomFieldDescriptor
	{
        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
		string Caption { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
		string DataType { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
		string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
		string Format { get; set; }

        /// <summary>
        /// Gets or sets the legal values.
        /// </summary>
        /// <value>The legal values.</value>
		string LegalValues { get; set; }

        /// <summary>
        /// Gets or sets the length of the max.
        /// </summary>
        /// <value>The length of the max.</value>
		int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
		string Name { get; set; }
	}
}