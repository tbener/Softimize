using System;

namespace CollectionsExercise
{
    public interface IBaseCollection<T> where T : IComparable<T>
    {
        event EventHandler OnItemAdded;
        event EventHandler OnItemRemoved;
        int Count { get; }
        new void Add(T item);
        T Remove();
        T this[int index] { get; set; }
    }
}