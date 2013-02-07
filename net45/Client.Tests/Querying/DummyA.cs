using System.ComponentModel;

namespace Gecko.NCore.Client.Tests.Querying
{
    public class DummyA: INotifyPropertyChanged
    {
    	private DummyB _b1;
    	public DummyB B1
    	{
    		get { return _b1; }
    		set
    		{
    			_b1 = value;
				OnPropertyChanged("B1");
    		}
    	}

    	private DummyB _b2;
    	public DummyB B2
    	{
    		get { return _b2; }
    		set
    		{
    			_b2 = value;
				OnPropertyChanged("B2");
    		}
    	}

    	private int _id;

    	public int Id
    	{
    		get { return _id; }
    		set
    		{
    			_id = value;
    			OnPropertyChanged("Id");
    		}
    	}

    	protected virtual void OnPropertyChanged(string propertyName)
    	{
    		var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
    	}

    	public event PropertyChangedEventHandler PropertyChanged;
    }
}