namespace Assignment7
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Collection of objects that does not hold two of the same object.
    /// Invariant: Cannot hold duplicates of a single object.
    /// </summary>
    /// <typeparam name="T">Type of objects the set can hold.</typeparam>
    public class Set<T> : Collection<T>
    {
        /// <summary>
        /// Constructor for class set.
        /// </summary>
        public Set()
        {
            Stack = new Stack<T>();
        }

        /// <summary>
        /// Adds an object to the set.
        /// </summary>
        /// <param name="obj">The object to be added to the set. This cannot be null.</param>
        public override void Add(T obj)
        {
            Contract.Ensures(Contract.OldValue(Contains(obj)) ? Size == Contract.OldValue(Size) : Size == Contract.OldValue(Size) + 1);
            if (!Contains(obj))
            {
                Stack.Push(obj);
            }

            OnCollectionChanged();
        }

        /// <summary>
        /// Removes an object from the set.
        /// </summary>
        /// <param name="obj">The object to be removed from the set. This cannot be null.</param>
        public override void Remove(T obj)
        {
            Contract.Ensures(!Contains(obj));
            Contract.Ensures(Contract.OldValue(Contains(obj)) ? Size == Contract.OldValue(Size) - 1 : Size == Contract.OldValue(Size));

            Stack.Extract(obj);
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
            Contract.Ensures(Contract.Result<Collection<U>>().GetType() == new Set<U>().GetType());
            var newSet = new Set<U>();
            foreach (T o in Stack)
            {
                newSet.Add(f(o));
            }

            return newSet;
        }

        /// <summary>
        /// No element is equal to another element in the set.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(ObjectInvariantHelper());
        }

        /// <summary>
        /// Helper method for ObjectInvariant.
        /// </summary>
        /// <returns>
        /// True if the Invariant holds, false if not.
        /// </returns>
        [Pure]
        private bool ObjectInvariantHelper()
        {
            var result = true;

            // This could probably have been done more efficiently.
            var temp = new Stack<T>();
            while (!Stack.IsEmpty())
            {
                temp.Push(Stack.Pop());
                if (Contains(temp.Top()))
                {
                    // If the remaining stack contains the object that was just taken out, invariant is not ok.
                    result = false;
                }
            }

            while (!temp.IsEmpty())
            {
                Stack.Push(temp.Pop());
            }

            return result;
        }
    }
}
