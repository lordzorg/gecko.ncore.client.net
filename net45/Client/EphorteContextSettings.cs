namespace Gecko.NCore.Client
{
    /// <summary>
    /// Class EphorteContextSettings
    /// </summary>
	public class EphorteContextSettings
	{
		private readonly ClientSettings _objectModelService = new ClientSettings();
		/// <summary>
		/// Gets the object model service settings.
		/// </summary>
		public ClientSettings ObjectModelService
		{
			get { return _objectModelService; }
		}


		private readonly ClientSettings _functionsService = new ClientSettings();
		/// <summary>
		/// Gets the functions service settings.
		/// </summary>
		/// <value>The functions service.</value>
		public ClientSettings FunctionsService
		{
			get { return _functionsService; }
		}

		private readonly ClientSettings _documentsService = new ClientSettings();
		/// <summary>
		/// Gets the documents service settings.
		/// </summary>
		/// <value>The documents service.</value>
		public ClientSettings DocumentsService
		{
			get { return _documentsService; }
		}

        private readonly ClientSettings _metadataService = new ClientSettings();
        /// <summary>
        /// Gets the documents service settings.
        /// </summary>
        /// <value>The documents service.</value>
        public ClientSettings MetadataService
        {
            get { return _metadataService; }
        }
	}
}