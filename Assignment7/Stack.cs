namespace Assignment7
{
    using System.Collections;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A stack data structure.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements in the stack.
    /// </typeparam>
    public class Stack<T> : IEnumerable
    {
        /// <summary>
        /// Internal representation of the stack.
        /// </summary>
        private T[] stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class.
        /// </summary>
        public Stack()
        {
            stack = new T[1];
            this.Size = 0;
        }

        /// <summary>
        /// Gets Size of the Stack.
        /// </summary>
        [Pure]
        public uint Size { get; private set; }

        /// <summary>
        /// Checks the top element in the stack.
        /// </summary>
        /// <returns>
        /// Element at the top of the stack.
        /// </returns>
        [Pure]
        public T Top()
        {
            return stack[this.Size - 1];
        }

        /// <summary>
        /// Checks if the stack is empty.
        /// </summary>
        /// <returns>
        /// True if the stack is empty, false if it contains elements.
        /// </returns>
        [Pure]
        public bool IsEmpty()
        {
            return this.Size <= 0;
        }

        /// <summary> Takes off the top element of the stack.</summary>
        /// <returns>The element at the top of the stack.</returns>
        public T Pop()
        {
            if ((this.Size - 1) < stack.Length / 4)
            {
                Resize();
            }

            return stack[--this.Size];
        }

        /// <summary> Puts an item at the top of the stack.</summary>
        /// <param name="item">Item to be put on top of the stack.</param>
        public void Push(T item)
        {
            stack[this.Size] = item;
            this.Size++;
            Resize();
        }

        /// <summary>
        /// Takes out the top most element of the stack that is equal to the item given.
        /// </summary>
        /// <param name="item">
        /// The item to be extracted from the stack.
        /// </param>
        public void Extract(T item)
        {
            var temp = new Stack<T>();
            while (!this.IsEmpty())
            {
                if (this.Top().Equals(item))
                {
                    this.Pop();
                    break;
                }

                temp.Push(this.Pop());
            }

            while (!temp.IsEmpty())
            {
                this.Push(temp.Pop());
            }
        }

        /// <summary>
        /// ToString method for the Stack.
        /// </summary>
        /// <returns>
        /// The stack as a string.
        /// </returns>
        public override string ToString()
        {
            if (Size < 1)
            {
                return "[]";
            }
            string q = "[ ";
            for (int i = 0; i < Size - 1; i++)
            {
                q = q + this.stack[i] + ", ";
            }

            q = q + this.stack[Size - 1] + " ]";
            return q;
        }

        /// <summary>
        /// The stack as an Enumerator. 
        /// </summary>
        /// <returns>
        /// An Enumerator.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (Size < 1)
            {
                yield break;
            }

            int i = 0;
            while (i < Size)
            {
                yield return this.stack[i];
                i++;
            }
        }

        /// <summary>
        /// Resizes the stack to make space or save memory.
        /// </summary>
        private void Resize()
        {
            if (this.Size <= stack.Length / 4)
            {
                var temp = new T[stack.Length / 2];
                for (var i = 0; i < this.Size; i++)
                {
                    temp[i] = stack[i];
                }

                stack = temp;
            }
            else if (this.Size == stack.Length)
            {
                var temp = new T[this.stack.Length * 2];
                for (var i = 0; i < this.Size; i++)
                {
                    temp[i] = this.stack[i];
                }

                this.stack = temp;
            }
        }
    }
}
