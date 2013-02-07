using System;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client
{
	public class SavedChangesEventArgs : EventArgs
	{
		private readonly IStateManager _stateManager;

		public SavedChangesEventArgs(IStateManager stateManager)
		{
			_stateManager = stateManager;
		}

		public IStateManager StateManager
		{
			get { return _stateManager; }
		}
	}
}