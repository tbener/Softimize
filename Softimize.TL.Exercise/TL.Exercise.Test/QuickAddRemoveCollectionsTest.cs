using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xunit;
using Xunit.Abstractions;

namespace CollectionsExercise.Test
{
    public class QuickAddRemoveCollectionsTest
    {
        #region Private members

        readonly ITestOutputHelper _output;

        #endregion

        #region CTOR

        public QuickAddRemoveCollectionsTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        #endregion

        #region Tests

        /// <summary>
        /// Runs the test with a Person type of object
        /// </summary>
        [Fact]
        public void TestPersonCollections()
        {
            List<Person> lst = GetRandomPersonCollectionsData();
            QuickAddRemoveTest(lst, false);
        }

        /// <summary>
        /// Runs the test on an int type of object
        /// </summary>
        [Fact]
        public void TestValueCollections()
        {
            List<int> lst = GetRandomCollectionsData(100);
            QuickAddRemoveTest(lst);
        }

        #endregion

        #region Data initialization

        /// <summary>
        /// Provides a list of random numbers
        /// </summary>
        /// <param name="size">Indicates the required list size</param>
        /// <returns>A list of randomized numbers</returns>
        private static List<int> GetRandomCollectionsData(int size)
        {
            Random rnd = new Random();
            List<int> lst = new List<int>();

            for (int i = 0; i < size; i++)
            {
                lst.Add(rnd.Next(1, 1000));
            }

            return lst;
        }

        /// <summary>
        /// Provides a list of Person objects, with random ages
        /// </summary>
        /// <returns>A list of Person objects</returns>
        private static List<Person> GetRandomPersonCollectionsData()
        {
            Random rnd = new Random();
            List<Person> lst = new List<Person>
            {
                new Person("Rafi", rnd.Next(1, 100)),
                new Person("John", rnd.Next(1, 100)),
                new Person("Django", rnd.Next(1, 100)),
                new Person("Dave", rnd.Next(1, 100)),
                new Person("Eliahu", rnd.Next(1, 100)),
                new Person("Dan", rnd.Next(1, 100)),
                new Person("Audrey", rnd.Next(1, 100)),
                new Person("James", rnd.Next(1, 100)),
                new Person("Eyal", rnd.Next(1, 100))
            };

            return lst;
        }

        #endregion

        #region Help methods

        /// <summary>
        /// This is the gateway of the actual test.
        /// Given a list of items, it initializes the 2 types of collection - QuickAdd and QuickRemove
        /// and send them to the TestCollection method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listTest">The list of items to be added to the collection</param>
        /// <param name="displayTimes">Determines whether to display the time elapsed for every operation</param>
        private void QuickAddRemoveTest<T>(List<T> listTest, bool displayTimes = true) where T : IComparable<T>
        {
            IBaseCollection<T> quickAddCollection = new QuickAddCollection<T>();
            IBaseCollection<T> quickRemoveCollection = new QuickRemoveCollection<T>();

            _output.WriteLine("Start QuickAddCollection Test:\n------");
            TestCollection(quickAddCollection, listTest, displayTimes);
            _output.WriteLine("------\nStart QuickRemoveCollection Test:\n------");
            TestCollection(quickRemoveCollection, listTest, displayTimes);
        }


        /// <summary>
        /// A generic test, regardless of the item type. Works by the following steps:
        /// 1. Adding the items in the list to the collection (show times if required)
        /// 2. Removing all items and test correctness by verifying that every item removed is smaller or
        /// equals to the one that was removed before it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The collection to test</param>
        /// <param name="list">A list of items of corresponded type</param>
        /// <param name="timeTrace">Determines whether to display the time elapsed for every operation</param>
        private void TestCollection<T>(IBaseCollection<T> col, List<T> list, bool timeTrace = true) where T : IComparable<T>
        {
            Stopwatch sw = new Stopwatch();
            foreach (T item in list)
            {
                sw.Start();
                col.Add(item);
                sw.Stop();
                if (timeTrace)
                    _output.WriteLine($"Add time: {sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms");
            }

            sw.Start();
            T item1 = col.Remove();
            sw.Stop();
            if (timeTrace)
                _output.WriteLine($"Remove time: {sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms");

            while (col.Count > 0)
            {
                sw.Start();
                T item2 = col.Remove();
                sw.Stop();
                if (timeTrace)
                    _output.WriteLine($"Remove time: {sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms");

                // TEST CORRECTNESS
                // Verify that the previous item removed is not smaller than the current one.
                // If the test fails we display the item values and the current state.
                Assert.True(item1.CompareTo(item2) >= 0, $"{item1}, {item2}: cmp={item1.CompareTo(item2)} ({col.GetType().Name}, {col.Count} items left)");
                item1 = item2;
            }
        }

        #endregion


    }


}
