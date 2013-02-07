using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client.Tests.Querying
{
	public class FakeDataObjectCollection<TDataObject>: Collection<TDataObject>, IDataObjectCollection<TDataObject>
	{
		private readonly Expression<Func<TDataObject, bool>> _predicate;

		public FakeDataObjectCollection(Expression<Func<TDataObject, bool>> predicate)
		{
			_predicate = predicate;
		}

		public void Load()
		{
			IsLoaded = true;
		}

	    public Task LoadAsync()
	    {
	        var taskCompletionSource = new TaskCompletionSource<bool>();
            IsLoaded = true;
            taskCompletionSource.SetResult(true);
	        return taskCompletionSource.Task;
	    }

	    public bool IsLoaded
		{
			get; private set;
		}

		public IQueryable<TDataObject> AsQueryable()
		{
			return this.Where(_predicate.Compile()).AsQueryable();
		}

		IQueryable IDataObjectCollection.AsQueryable()
		{
			return AsQueryable();
		}

	    public IQueryProvider QueryProvider { get; set; }
	}
}