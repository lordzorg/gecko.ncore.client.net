using System.ComponentModel;
using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client.Tests.Querying
{
    public class DummyB: INotifyPropertyChanged
    {
    	private readonly FakeDataObjectCollection<DummyA> _dummyAs = new FakeDataObjectCollection<DummyA>(x => true);

		public DummyB()
		{
			_dummyAs.Add(new DummyA { B1 = this, B2 = this, Id = Id});
			_dummyAs.Add(new DummyA { B1 = this, B2 = this, Id = Id });
			_dummyAs.Add(new DummyA { B1 = this, B2 = this, Id = Id });
			_dummyAs.Add(new DummyA { B1 = this, B2 = this, Id = Id });
			_dummyAs.Add(new DummyA { B1 = this, B2 = this, Id = Id });
			_dummyAs.Add(new DummyA { B1 = this, B2 = this, Id = Id });
		}

    	public DummyA A { get; set; }

        public int Id
        {
            get;
            set;
        }

        public string Tittel { get; set; }

    	public IDataObjectCollection<DummyA> DummyAs
    	{
    		get
    		{
    			return _dummyAs;
    		}
    	}

        public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
    }
}