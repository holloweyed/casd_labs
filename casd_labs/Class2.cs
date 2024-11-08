using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    public class MyStack<T> : MyVector<T>
    {
        public void Push(T item)
        {
            Add(item);
        }

        public T Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is Empty, cannot pop.");
            }
            T item = Get(Size() - 1);
            Remove(Size() - 1);
            return item;
        }

        public T Peek()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is Empty, cannot peek.");
            }

            return Get(Size() - 1);
        }

        public bool Empty()
        {
            return Size() == 0;
        }

        public int Search(T item)
        {
            for (int i = 1; i < Size(); i++)
            {
                if (item.Equals(Get(Size() - i)))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
