using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.StateTracking
{
	internal class StateManager : IStateManager
	{
		public IQueryProvider QueryProvider { get; private set; }
	
		public StateManager(Func<StateManager, IQueryProvider> queryProviderResolver)
		{
			QueryProvider = queryProviderResolver(this);
		}

		private void ProcessDataObjectCollections(object dataObject, IQueryProvider queryProvider)
		{
			var dataObjectType = dataObject.GetType();
			foreach (var property in dataObjectType.GetProperties().Where(x => typeof(IDataObjectCollection).IsAssignableFrom(x.PropertyType)))
			{
				var dataObjectCollection = (IDataObjectCollection)property.GetValue(dataObject, null);
				dataObjectCollection.QueryProvider = queryProvider;
			}
		}

		private readonly HashSet<StateEntry> _entries = new HashSet<StateEntry>();

		public IQueryable<StateEntry> Entries
		{
			get { return _entries.AsQueryable(); }
		}


		public void Add(object dataObject)
		{
			if (dataObject == null)
				throw new ArgumentNullException("dataObject");

			ProcessDataObjectCollections(dataObject, QueryProvider);

			_entries.Add(new StateEntry(dataObject, DataObjectState.Added));
		}

		public void Remove(object dataObject)
		{
			if (dataObject == null)
				throw new ArgumentNullException("dataObject");

			var stateEntry = _entries.FirstOrDefault(x => x.DataObject == dataObject);
			if (stateEntry == null)
			{
				_entries.Add(new StateEntry(dataObject, DataObjectState.Removed));
			}
			else if (stateEntry.State == DataObjectState.Added)
			{
				_entries.Remove(stateEntry);
			}
			else
			{
				stateEntry.MarkAsRemoved();
			}
		}

		public void Attach(object dataObject)
		{
			if (dataObject == null)
				throw new ArgumentNullException("dataObject");

			ProcessDataObjectCollections(dataObject, QueryProvider);
			_entries.Add(new StateEntry(dataObject, DataObjectState.Modified));
		}

		public void Detach(object dataObject)
		{
			if (dataObject == null)
				throw new ArgumentNullException("dataObject");

			_entries.RemoveWhere(x => x.DataObject == dataObject);
			ProcessDataObjectCollections(dataObject, null);
		}

		public void WeakAttach(object dataObject)
		{
			if (dataObject == null)
				throw new ArgumentNullException("dataObject");

			ProcessDataObjectCollections(dataObject, QueryProvider);

			var notifyPropertyChanged = dataObject as INotifyPropertyChanged;
			if (notifyPropertyChanged == null)
				throw new InvalidOperationException(Resources.ChangeTrackingIsOnlySupportedForINotifyPropertyChanged);

			notifyPropertyChanged.PropertyChanged += WeaklyAttachedDataObjectChanged;
		}

		private void WeaklyAttachedDataObjectChanged(object sender, PropertyChangedEventArgs e)
		{
			var notifyPropertyChanged = sender as INotifyPropertyChanged;
			if (notifyPropertyChanged == null)
				throw new InvalidOperationException(Resources.ChangeTrackingIsOnlySupportedForINotifyPropertyChanged);

			notifyPropertyChanged.PropertyChanged -= WeaklyAttachedDataObjectChanged;

			ProcessDataObjectCollections(sender, QueryProvider);

			var stateEntry = new StateEntry(sender, DataObjectState.Modified);
			_entries.Add(stateEntry);
		}
	}
}
