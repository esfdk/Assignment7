namespace Assignment7
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A list of items in no order but in a specific sequence.
    /// </summary>
    /// /// <typeparam name="T">The type of objects the list should hold.</typeparam>
    public class List<T> : Bag<T>
    {
        /// <summary>
        /// Constructor for class set.
        /// </summary>
        public List()
        {
            Stack = new Stack<T>();
        }

        /// <summary>
        /// Finds the first index of the object in the list.
        /// </summary>
        /// <param name="obj">The object to be searched for. This cannot be null.</param>
        /// <returns>The first index of the object. If object is not in the list, this will be size.</returns>
        [Pure]
        public uint FirstIndex(T obj)
        {
            Contract.Requires(obj != null);
            Contract.Ensures(Contract.OldValue(Contains(obj)) ? Get(Contract.Result<uint>()).Equals(obj) : true);

            var temp = new Stack<T>();
            while (!Stack.IsEmpty())
            {
                temp.Push(Stack.Pop());
            }

            uint index = 0;
            while (!temp.IsEmpty())
            {
                if (temp.Top().Equals(obj))
                {
                    break;
                }

                index++;
                Stack.Push(temp.Pop());
            }

            while (!temp.IsEmpty())
            {
                Stack.Push(temp.Pop());
            }

            return index;
        }

        /// <summary>
        /// Finds the last index of the object in the list.
        /// </summary>
        /// <param name="obj">The object to be searched for. This cannot be null.</param>
        /// <returns>The last of the object. If object is not in the list, this will be size.</returns>
        [Pure]
        public uint LastIndex(T obj)
        {
            Contract.Requires(obj != null);
            Contract.Ensures(Contract.OldValue(Contains(obj)) ? Get(Contract.Result<uint>()).Equals(obj) : true);

            var temp = new Stack<T>();
            uint index = Size;
            while (!Stack.IsEmpty())
            {
                if (Stack.Top().Equals(obj))
                {
                    index = Size - 1;
                    break;
                }

                temp.Push(Stack.Pop());
            }

            while (!temp.IsEmpty())
            {
                Stack.Push(temp.Pop());
            }

            return index;
        }

        /// <summary>
        /// Get the item on a specific index.
        /// </summary>
        /// <param name="i">The index of the item to get. This cannot be more than Size - 1.</param>
        /// <returns>The item at the specified index.</returns>
        [Pure]
        public T Get(uint i)
        {
            Contract.Requires(i < Size);
            Contract.Ensures(Contains(Contract.Result<T>()));
            var temp = new Stack<T>();
            uint index = Size - 1;
            T item;
            while (index > i)
            {
                temp.Push(Stack.Pop());
                index--;
            }

            item = Stack.Top();
            while (!temp.IsEmpty())
            {
                Stack.Push(temp.Pop());
            }

            return item;
        }

        /// <summary>
        /// Adds an item to the end of the list.
        /// </summary>
        /// <param name="obj">The object to be added. This cannot be null.</param>
        public override void Add(T obj)
        {
            Contract.Ensures(Contains(obj));
            Contract.Ensures(LastIndex(obj) == Size - 1);
            Contract.Ensures(Contract.OldValue(Size) + 1 == Size);

            Stack.Push(obj);
            OnCollectionChanged();
        }

        /// <summary>
        /// Adds an object a specific index.
        /// </summary>
        /// <param name="obj">Object to be added to the list. Must not be null.</param>
        /// <param name="i">The index where the object should be added. Must be Size - 1 or less.</param>
        public void AddAt(T obj, uint i)
        {
            Contract.Requires(obj != null);
            Contract.Requires(i < Size);
            Contract.Ensures(Get(i).Equals(obj));
            Contract.Ensures(Size == Contract.OldValue(Size) + 1);
            Contract.Ensures(Contains(obj));

            var temp = new Stack<T>();
            uint index = Size - 1;
            while (index >= i)
            {
                temp.Push(Stack.Pop());
                index--;
            }

            Stack.Push(obj);
            while (!temp.IsEmpty())
            {
                Stack.Push(temp.Pop());
            }

            OnCollectionChanged();
        }

        /// <summary>
        /// Removes the item at a specific index.
        /// </summary>
        /// <param name="i">Must be less than Size.</param>
        public void RemoveAt(uint i)
        {
            Contract.Requires(i < Size);
            Contract.Ensures(Count(Contract.OldValue(Get(i))) >= 1
                                 ? Count(Contract.OldValue(Get(i))) == Contract.OldValue(Count(Contract.OldValue(Get(i)))) - 1
                                 : Count(Contract.OldValue(Get(i))) == 0);
            Contract.Ensures(Get(i).Equals(Contract.OldValue(Get(i + 1))));

            var temp = new Stack<T>();
            uint index = Size - 1;
            while (index > i)
            {
                temp.Push(Stack.Pop());
                index--;
            }

            Stack.Pop();
            while (!temp.IsEmpty())
            {
                Stack.Push(temp.Pop());
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
        /// The resulting bag.
        /// </returns>
        public override Collection<U> Map<U>(Func<T, U> f)
        {
            Contract.Ensures(Contract.Result<Collection<U>>().GetType() == new List<U>().GetType());

            var newBag = new List<U>();
            foreach (T o in Stack)
            {
                newBag.Add(f(o));
            }

            return newBag;
        }
    }
}
