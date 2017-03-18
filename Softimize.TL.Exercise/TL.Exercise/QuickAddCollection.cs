using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsExercise
{
    /// <summary>
    /// In QuickAddCollection class we override the GetIndexToRemove method.
    /// This ensures that the Add operation is kept simple (O(1)). The Add operation will always add the item as
    /// the last one, without any further manipulations.
    /// The Remove operation will do the O(n) complexity, by searching for the item with the maximum value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuickAddCollection<T> : BaseCollection<T> where T : IComparable<T>
    {
        protected override int GetIndexToRemove()
        {
            int maxIndex = 0;
            for (int i = 1; i < Count; i++)
            {
                if (this[i].CompareTo(this[maxIndex]) > 0) maxIndex = i;
            }
            return maxIndex;
        }
    }

   
}
