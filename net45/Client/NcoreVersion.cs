using System;
using System.Configuration;

namespace Gecko.NCore.Client
{
	/// <summary>
	/// Specify version in constructor.
	/// 
	/// Retrieves ncoreversion from appsettings key "EphorteContext:NCoreVersion"
	/// 
	/// Sets version 0.0.1 if no version is configured.
	/// </summary>
	public class NcoreVersion
	{
		/// <summary>
		/// Represents the most backwards compatible version of ncore. Internally represented by the version 0.0.1
		/// </summary>
		public static NcoreVersion Default
		{
			get { return new NcoreVersion(new Version(0, 0, 1)); }
		}

		/// <summary>
		/// You can specify an ncore version in the AppSettings section using the key: 'EphorteContext:NCoreVersion'
		/// 
		/// This value must be parseable by the System.Version class
		/// </summary>
		public static NcoreVersion Configured
		{
			get
			{
				var version = GetNcoreVersionFromConfig();
				return version == null ? Default : new NcoreVersion(version);
			}
		}

		/// <summary>
		/// Use this ncoreversion if you always want to use the latest ncore version.
		/// 
		/// Internally represented by 'new Version(int.MaxValue, int.MaxValue, int.MaxValue)'
		/// </summary>
		public static NcoreVersion Latest
		{
			get
			{
				return new NcoreVersion(new Version(int.MaxValue, int.MaxValue, int.MaxValue));
			}
		}

		public Version Version { get; private set; }

		public NcoreVersion(Version version)
		{
			Version = version;
		}
		
		private static Version GetNcoreVersionFromConfig()
		{
			const string ncoreVersionConfigKey = "EphorteContext:NCoreVersion";
			var ncoreVersionString = ConfigurationManager.AppSettings[ncoreVersionConfigKey];

			if (string.IsNullOrEmpty(ncoreVersionString))
				return null;

			try
			{
				return new Version(ncoreVersionString);
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(string.Format("The value '{0}' for configuration key '{1}' was not a valid version number. It must be on the form Major.Minor.Patch (ex. 2.1.3).", ncoreVersionString, ncoreVersionConfigKey), ex);
			}
		}

		// NCore 3.1.3 and 5.1.3 have implemented support for stripping away quotes
		// We default to false since that is the most backwards compatible configuration
		// Ref changeset 19122
		public bool SupportFieldQuoting()
		{
			if ((Version >= new Version(3, 1, 3) && Version < new Version(4, 0)) ||
				Version >= new Version(5, 1, 3))
				return true;

			return false;
		}
	}
}