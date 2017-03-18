using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsExercise
{
    /// <summary>
    /// BaseCollection holds the basic operations and maintenance of the collection, which are:
    /// Add: adds the given item to the collection.
    /// Remove: removes the last item and return it.
    /// Publish: 2 event handles are used for publishing the Add\Remove operations to whoever registered.
    /// 
    /// The class inherites from Collection<T> to maintain the basic collection usage.
    /// </summary>
    /// <typeparam name="T">Can be any type that implements the IComparable interface.</typeparam>
    public class BaseCollection<T> : Collection<T>, IBaseCollection<T> where T : IComparable<T>
    {
        #region Event

        public event EventHandler OnItemAdded;
        public event EventHandler OnItemRemoved;

        #endregion

        // The virtual methods are designed to be overriden by the derived class
        // and by that determine where will the comlexity lay.
        #region Virtual methods

        protected virtual int GetIndexToAdd(T item)
        {
            return Count;
        }

        protected virtual int GetIndexToRemove()
        {
            return Count - 1;
        }

        #endregion

        #region Main operations methods

        public new void Add(T item)
        {
            lock (this)
            {
                InsertItem(GetIndexToAdd(item), item);
            }
            OnItemAdded?.Invoke(this, new ItemEventArgs<T>(item));
        }

        public T Remove()
        {
            T item;
            lock (this)
            {
                item = this[GetIndexToRemove()];
                Remove(item);
            }
            OnItemRemoved?.Invoke(this, new ItemEventArgs<T>(item));
            return item;
        }

        #endregion
    }


    /// <summary>
    /// ItemEventArgs derives from EventArgs and used as the collections events argument
    /// </summary>
    public class ItemEventArgs<T> : EventArgs
    {
        public ItemEventArgs(T item)
        {
            Item = item;
        }
        public T Item { get; set; }
    }
}
