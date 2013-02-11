using System.Configuration;
using System.Diagnostics;

namespace Gecko.NCore.Client
{
	/// <summary>
	/// Provides an identity for the ePhorte© Integration Services
	/// </summary>
	[DebuggerDisplay("Username: {Username}, Password: {Password}, Role: {Role}, Database: {Database}, ExternalSystemName: {ExternalSystemName}")]
	public class EphorteContextIdentity
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="EphorteContextIdentity" /> class.
        /// </summary>
		public EphorteContextIdentity()
		{
			ExternalSystemName = ConfigurationManager.AppSettings["ExternalSystemName"];
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="EphorteContextIdentity" /> class.
        /// </summary>
        /// <param name="externalSystemName">Name of the external system.</param>
		public EphorteContextIdentity(string externalSystemName)
		{
			ExternalSystemName = externalSystemName;
		}

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the database.
		/// </summary>
		/// <value>The database.</value>
		public string Database { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>The role.</value>
		public string Role { get; set; }

		/// <summary>
		/// Name used to validate against the Eis-license on Ephorte NCore server.
		/// It should match the ExternalSystemName defined on your license configured on the Ephorte NCore server.
		/// The same name should normally be used on all service adapters.
		/// </summary>
		/// <value>The ExternalSystemName.</value>
		public string ExternalSystemName { get; set; }
	}
}