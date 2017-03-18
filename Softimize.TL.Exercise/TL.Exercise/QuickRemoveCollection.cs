using System;
using CollectionsExercise;

namespace CollectionsExercise
{
    /// <summary>
    /// In QuickRemoveCollection class we override the GetIndexToAdd method.
    /// This ensures that the items are kept sorted by inserting the new item to the right place in the collection.
    /// This operation places the comlexity (O(n)) on the Add process.
    /// Doing that leaves the lower complexity (O(1)) to the Remove operation, which will allways need to take
    /// the last item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuickRemoveCollection<T> : BaseCollection<T> where T : IComparable<T>
    {
        protected override int GetIndexToAdd(T item)
        {
            for (int i = Count; i > 0; i--)
            {
                if (item.CompareTo(this[i-1]) > 0) return i;
            }
            return 0;
        }
    }
}
