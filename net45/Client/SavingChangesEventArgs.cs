using System;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client
{
    /// <summary>
    /// Class SavingChangesEventArgs
    /// </summary>
	public class SavingChangesEventArgs : EventArgs
	{
		private readonly IStateManager _stateManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingChangesEventArgs" /> class.
        /// </summary>
        /// <param name="stateManager">The state manager.</param>
		public SavingChangesEventArgs(IStateManager stateManager)
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