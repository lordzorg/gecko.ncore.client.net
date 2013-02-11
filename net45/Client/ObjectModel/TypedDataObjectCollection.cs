using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gecko.NCore.Client.Querying;

#if NET45
using System.Threading.Tasks;
#endif

namespace Gecko.NCore.Client.ObjectModel
{
    /// <summary>
    /// Provides support for lazy loading data object collections.
    /// </summary>
    /// <typeparam name="TDataObject">The type of the data object.</typeparam>
    public class TypedDataObjectCollection<TDataObject> : DataObjectCollection, IDataObjectCollection<TDataObject> where TDataObject : class
    {
        private readonly Expression<Func<TDataObject, bool>> _predicate;
        private readonly List<TDataObject> _dataObjects = new List<TDataObject>();


        /// <summary>
        /// Initializes a new instance of the <see cref="TypedDataObjectCollection{TDataObject}" /> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public TypedDataObjectCollection(Expression<Func<TDataObject, bool>> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Loads this collection.
        /// </summary>
        protected override void LoadCore()
        {
            var query = new DataObjectQuery<TDataObject>(QueryProvider).Where(_predicate);
            _dataObjects.AddRange(query.Lazy());
        }

#if NET45
        /// <summary>
        /// Loads the core async.
        /// </summary>
        /// <returns>Task.</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected override async Task LoadCoreAsync()
        {
            if (!IsAsync)
                throw new NotSupportedException();

            var query = new DataObjectQuery<TDataObject>(QueryProvider).Where(_predicate);
            _dataObjects.AddRange(await query.LazyAsync());
        }

        private bool IsAsync
        {
            get { return !(QueryProvider is DataObjectQueryProvider); }
        }
#endif

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<TDataObject> GetEnumerator()
        {
            Load();
            return _dataObjects.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns the relationship as a <see cref="IQueryable{TDataObject}"/>.
        /// </summary>
        /// <returns></returns>
        public new IQueryable<TDataObject> AsQueryable()
        {
            return (IQueryable<TDataObject>)AsQueryableCore();
        }

        /// <summary>
        /// Ases the queryable core.
        /// </summary>
        /// <returns>IQueryable.</returns>
        protected override IQueryable AsQueryableCore()
        {
            if (IsLoaded)
                return _dataObjects.AsQueryable();

            return new DataObjectQuery<TDataObject>(QueryProvider).Where(_predicate);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get
            {
                if (IsLoaded)
                    return _dataObjects.Count;

                var query = new DataObjectQuery<TDataObject>(QueryProvider).Where(_predicate);
                return query.Count();
            }
        }

#if !NET45
        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        ///                 </param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        ///                 </exception>
        public void Add(TDataObject item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. 
        ///                 </exception>
        public void Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        ///                 </param>
        public bool Contains(TDataObject item)
        {
            Load();
            return _dataObjects.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
        ///                 </param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.
        ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.
        ///                     -or-
        ///                 <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        ///                     -or-
        ///                     The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        ///                     -or-
        ///                     Type <typeparamref name="TDataObject"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        ///                 </exception>
        public void CopyTo(TDataObject[] array, int arrayIndex)
        {
            Load();
            _dataObjects.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        ///                 </param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        ///                 </exception>
        public bool Remove(TDataObject item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return true; }
        }
#endif
    }
}