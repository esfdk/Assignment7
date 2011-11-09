namespace Assignment7
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// A collection of elements.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the objects the collection contains.
    /// </typeparam>
    public abstract class Collection<T>
    {
        /// <summary>
        /// The stack used to implement the collection.
        /// </summary>
        protected Stack<T> Stack;

        /// <summary>
        /// Notifies when something in the collection changed.
        /// </summary>
        public event Handler CollectionChanged;

        /// <summary>
        /// Gets the size of the Collection.
        /// </summary>
        [Pure]
        public uint Size
        {
            get
            {
                return Stack.Size;
            }
        }

        /// <summary>
        /// Checks if the Collection contains the object.
        /// </summary>
        /// <param name="obj">
        /// The object to be searched for.
        /// </param>
        /// <returns>
        /// True if the object is contained, false if not.
        /// </returns>
        [Pure]
        public bool Contains(T obj)
        {
            Contract.Requires(obj != null);

            return Stack.Cast<T>().Contains(obj);
        }

        /// <summary>
        /// Counts how many instances of the specified object is in the collection.
        /// </summary>
        /// <param name="obj">
        /// The obj to be counted.
        /// </param>
        /// <returns>
        /// How many of the specified object is in the collection.
        /// </returns>
        [Pure]
        public uint Count(T obj)
        {
            Contract.Requires(obj != null);
            Contract.Ensures(true);

            uint count = 0;
            foreach (T o in Stack)
            {
                if (o.Equals(obj))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Is the collection empty?
        /// </summary>
        /// <returns>
        /// True if the collection is empty. False if not.
        /// </returns>
        public bool IsEmpty()
        {
            Contract.Ensures(Contract.Result<bool>() == (Size == 0));
            return Size == 0;
        }

        /// <summary>
        /// Adds the object to the collection.
        /// </summary>
        /// <param name="obj">
        /// The object to be added. Cannot be null..
        /// </param>
        public virtual void Add(T obj)
        {
            Contract.Requires(obj != null);
            Contract.Ensures(Contains(obj));

            Stack.Push(obj);
            OnCollectionChanged();
        }

        /// <summary>
        /// Removes the specific object from the collection.
        /// </summary>
        /// <param name="obj">
        /// Object to be removed. Cannot be null.
        /// </param>
        public virtual void Remove(T obj)
        {
            Contract.Requires(obj != null);
            Contract.Ensures(
                Contract.OldValue(Contains(obj))
                    ? Count(obj) == Contract.OldValue(Count(obj)) - 1 & Size == Contract.OldValue(Size) - 1
                    : Size == Contract.OldValue(Size));

            Stack.Extract(obj);
            OnCollectionChanged();

        }

        /// <summary>
        /// Removes all the objects from the collection.
        /// </summary>
        public void RemoveAll()
        {
            Contract.Ensures(Size == 0);
            while (!Stack.IsEmpty())
            {
                Stack.Pop();
            }

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
        /// The resulting collection.
        /// </returns>
        public abstract Collection<U> Map<U>(Func<T, U> f);

        /// <summary>
        /// Determines if a predicate holds for all elements in the collection.
        /// </summary>
        /// <param name="predicate">
        /// The predicate used in the ForAll.
        /// </param>
        /// <returns>
        /// True if predicate holds for all elements, false if not.
        /// </returns>
        public bool ForAll(Predicate<T> predicate)
        {
            return Stack.Cast<T>().All(o => predicate(o));
        }

        /// <summary>
        /// Determines if a predicate holds for any element in the collection.
        /// </summary>
        /// <param name="predicate">
        /// The predicate used in the exist.
        /// </param>
        /// <returns>
        /// True if predicate holds for an element, false if not.
        /// </returns>
        public bool Exists(Predicate<T> predicate)
        {
            return Stack.Cast<T>().Any(o => predicate(o));
        }

        /// <summary>
        /// Determines if a predicate holds for one and only one element in the collection.
        /// </summary>
        /// <param name="predicate">
        /// The predicate used in the ExistUnique.
        /// </param>
        /// <returns>
        /// True if predicate holds for one and only one element, false if not.
        /// </returns>
        public bool ExistsUnique(Predicate<T> predicate)
        {
            return Stack.Cast<T>().Count(o => predicate(o)) == 1;
        }

        /// <summary>
        /// Called when the collection changes.
        /// </summary>
        protected void OnCollectionChanged()
        {
            Handler handler = CollectionChanged;
            if (handler != null)
            {
                handler(this);
            }
        }
    }
}
