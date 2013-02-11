using System;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client
{
    /// <summary>
    /// Class SavedChangesEventArgs
    /// </summary>
	public class SavedChangesEventArgs : EventArgs
	{
		private readonly IStateManager _stateManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedChangesEventArgs" /> class.
        /// </summary>
        /// <param name="stateManager">The state manager.</param>
		public SavedChangesEventArgs(IStateManager stateManager)
		{
			_stateManager = stateManager;
		}

        /// <summary>
        /// Gets the state manager.
        /// </summary>
        /// <value>The state manager.</value>
		public IStateManager StateManager
		{
			get { return _stateManager; }
		}
	}
}