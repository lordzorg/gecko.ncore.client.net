using System.ComponentModel;
using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client.Tests.StateTracking
{
    public class TrackableObject: INotifyPropertyChanged
    {
        private string _trackableProperty;
        private IDataObjectCollection<TrackableObject> _dataObjectCollection;

        public string TrackableProperty
        {
            get { return _trackableProperty; }
            set
            {
                _trackableProperty = value;
                OnPropertyChanged("PropertyChanged");
            }
        }

        public IDataObjectCollection<TrackableObject> DataObjectCollection
        {
            get { return _dataObjectCollection ?? (_dataObjectCollection = new TypedDataObjectCollection<TrackableObject>(x => true)); }
            set { _dataObjectCollection = value; }
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}