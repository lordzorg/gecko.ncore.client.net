using System;
using System.ComponentModel;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.StateTracking
{
    /// <summary>
    /// 
    /// </summary>
    public class StateEntry
    {
        private readonly INotifyPropertyChanged _dataObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateEntry"/> class.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <param name="state">Type of the state.</param>
        internal StateEntry(object dataObject, DataObjectState state)
        {
            if (dataObject == null)
                throw new ArgumentNullException("dataObject");

            _dataObject = dataObject as INotifyPropertyChanged;

            if (_dataObject == null)
                throw new InvalidOperationException(Resources.ChangeTrackingIsOnlySupportedForINotifyPropertyChanged);

            State = state;
            if (state == DataObjectState.Clean)
            {
                _dataObject.PropertyChanged += DataObjectChanged;    
            }
        }

        /// <summary>
        /// Gets the data object.
        /// </summary>
        /// <value>The data object.</value>
        public object DataObject
        {
            get { return _dataObject; }
        }

        /// <summary>
        /// Gets or sets the type of the state.
        /// </summary>
        /// <value>The type of the state.</value>
        public DataObjectState State { get; private set; }

        private void DataObjectChanged(object sender, PropertyChangedEventArgs e)
        {
            State = DataObjectState.Modified;
            _dataObject.PropertyChanged -= DataObjectChanged;
        }

        internal void MarkAsRemoved()
        {
            if (State == DataObjectState.Removed)
                return;

            _dataObject.PropertyChanged -= DataObjectChanged;
            State = DataObjectState.Removed;
        }

        internal void MarkAsClean()
        {
            if (State == DataObjectState.Clean)
                return;

            _dataObject.PropertyChanged += DataObjectChanged;
            State = DataObjectState.Clean;
        }
    }

}