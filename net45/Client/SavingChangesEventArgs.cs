using System;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client
{
	public class SavingChangesEventArgs : EventArgs
	{
		private readonly IStateManager _stateManager;

		public SavingChangesEventArgs(IStateManager stateManager)
		{
			_stateManager = stateManager;
		}

		public IStateManager StateManager
		{
			get { return _stateManager; }
		}
	}
}