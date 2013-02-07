using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// Used to determine at what stage the <see cref="IDataObjectCollection{TDataObject}"/> is loaded.
    /// </summary>
    public enum LoadOption
    {
        /// <summary>
        /// The instance is populated on demand.
        /// </summary>
        Lazy,

        /// <summary>
        /// Ensures that the instance is populated on initial query.
        /// </summary>
        Greedy
    }
}