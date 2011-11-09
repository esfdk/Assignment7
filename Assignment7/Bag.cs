namespace Assignment7
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A collection of elements in no particular order or sequence.
    /// </summary>
    /// <typeparam name="T">
    /// The object type the collection needs to hold.
    /// </typeparam>
    public class Bag<T> : Collection<T>
    {
        /// <summary>
        /// Constructor for class set.
        /// </summary>
        public Bag()
        {
            Stack = new Stack<T>();
        }

        /// <summary>
        /// Adds the object to the bag.
        /// </summary>
        /// <param name="obj">
        /// The object to be added.
        /// </param>
        public override void Add(T obj)
        {
            Contract.Ensures(Contains(obj));
            Contract.Ensures(Size == Contract.OldValue(Size) + 1);
            Contract.Ensures(Count(obj) == Contract.OldValue(Count(obj)) + 1);
            Stack.Push(obj);
            OnCollectionChanged();
        }

        /// <summary>
        /// Applies a function to all elements in the Collection and produces a new Collection.
        /// </summary>
        /// <typeparam name="U">
        /// The type of the elements in the resulting collection.
        /// </typeparam>
        /// <param name="f">
        /// The function to be applied to the collection.
        /// </param>
        /// <returns>
        /// The resulting bag.
        /// </returns>
        public override Collection<U> Map<U>(Func<T, U> f)
        {
            Contract.Ensures(Contract.Result<Collection<U>>().GetType() == new Bag<U>().GetType());

            var newBag = new Bag<U>();
            foreach (T o in Stack)
            {
                newBag.Add(f(o));
            }

            return newBag;
        }
    }
}