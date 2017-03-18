using System;

namespace CollectionsExercise.Test
{
    /// <summary>
    /// A simple Person class for demonstration of Quick Add\Remove Collections.
    /// The class implements the IComparable interface.
    /// </summary>
    class Person : IComparable<Person>
    {
        #region CTOR

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        #endregion

        #region IComparable implementation

        public int CompareTo(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            return Age.CompareTo(person.Age);
        }

        #endregion

        #region Public properties

        public string Name { get; set; }
        public int Age { get; set; }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"{Name} ({Age})";
        }
         
        #endregion
    }
}
