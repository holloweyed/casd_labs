using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    public class MyArrayDeque<E>
    {
        private E[] elements;
        private int head;
        private int tail;

        public MyArrayDeque()
        {
            elements = new E[16];
            head = 0;
            tail = 0;
        }

        public MyArrayDeque(E[] a)
        {
            elements = new E[a.Length];
            Array.Copy(a, 0, elements, head, a.Length);
            tail = a.Length;
        }

        public MyArrayDeque(int numElements)
        {
            if (numElements <= 0)
                throw new ArgumentException("Capasity cannot be less than or equal to zero");

            elements = new E[numElements];
            head = 0;
            tail = 0;
        }

        public void Add(E e)
        {
            if (tail - head == elements.Length)
            {
                E[] array = new E[elements.Length * 2];
                for (int i = 0; i < tail + 1; i++) array[i] = elements[i];
                elements = array;
                elements[tail++] = e;
            }
            else
            {
                elements[tail++] = e;
            }
        }

        public void AddAll(E[] a)
        {
            foreach (E item in a)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            Array.Clear(elements, 0, elements.Length);
            head = 0;
            tail = 0;
        }

        public bool Contains(object o)
        {
            for (int i = head; i < tail + 1; i++)
            {
                foreach (E t in elements)
                {
                    if (Equals(t, o)) return true;
                }
            }
            return false;
        }

        public bool ContainsAll(E[] a)
        {
            foreach (E item in a)
            {
                if (!Contains(item))
                    return false;
            }
            return true;
        }

        public bool IsEmpty()
        {
            return tail - head == 0;
        }

        private int FindIndex(object o)
        {
            for (int i = head; i < tail + 1; i++)
            {
                if (o.Equals(elements[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Remove(object o)
        {
            for (int i = 0; i < tail-head; i++)
            {
                int index = (head + i) % elements.Length;
                if (elements[index]?.Equals(o) == true)
                {
                    RemoveAt(index);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAll(E[] a)
        {
            foreach (E item in a)
            {
                Remove(item);
            }
        }

        public void RetainAll(E[] a)
        {
            for (int i = 0; i < a.Length; i++) elements[i] = a[i];
            tail = a.Length;
        }

        public int Size()
        {
            return tail - head;
        }

        public E[] ToArray()
        {
            E[] newArray = new E[tail - head];
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = elements[head + i];
            }
            return newArray;
        }

        public E[] ToArray(E[] a)
        {
            if (a == null || a.Length < tail - head)
            {
                return ToArray();
            }
            for (int i = 0; i < tail - head; i++)
            {
                a[i] = elements[head + i];
            }
            return a;
        }

        public E Element()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Deque is empty.");
            return elements[head];
        }

        public bool Offer(E obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public E Peek()
        {
            return IsEmpty() ? default(E) : elements[head];
        }

        public E Poll()
        {
            if (IsEmpty())
                return default(E);
            E item = elements[head];
            head = head + 1;
            return item;
        }

        public void AddFirst(E obj)
        {
            if (head == 0)
            {
                int newCapacity = elements.Length * 2;
                E[] newElements = new E[newCapacity];
                for (int i = 1; i < elements.Length + 1; i++)
                {
                    newElements[i] = elements[i - 1];
                }
                newElements[0] = obj;
                elements = newElements;

            }
            else
            {
                head = head - 1;
                elements[head] = obj;
            }
        }

        public void AddLast(E obj)
        {
            Add(obj);
        }

        public E GetFirst()
        {
            return Element();
        }

        public E GetLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Deque is empty.");
            return elements[tail];
        }

        public bool OfferFirst(E obj)
        {
            if (tail - head >= elements.Length) return false;
            AddFirst(obj);
            return true;
        }

        public bool OfferLast(E obj)
        {
            return Offer(obj);
        }

        public E Pop()
        {
            return Poll();
        }

        public void Push(E obj)
        {
            if (head == 0)
            {
                int newCapacity = elements.Length * 2;
                E[] newElements = new E[newCapacity];
                for (int i = 1; i < elements.Length + 1; i++)
                {
                    newElements[i] = elements[i - 1];
                }
                newElements[0] = obj;
                elements = newElements;
                tail++;

            }
            else
            {
                head = head - 1;
                elements[head] = obj;
            }
        }

        public E PeekFirst()
        {
            return Peek();
        }

        public E PeekLast()
        {
            if (IsEmpty())
                return default(E);
            return elements[tail];
        }

        public E PollFirst()
        {
            return Poll();
        }

        public E PollLast()
        {
            if (IsEmpty())
                return default(E);

            tail = tail - 1;
            E item = elements[tail];
            elements[tail] = default(E);
            return item;
        }

        public E RemoveLast()
        {
            return PollLast();
        }

        public E RemoveFirst()
        {
            return Poll();
        }

        public bool RemoveLastOccurrence(object obj)
        {
            for (int i = tail; i >= head; i--)
            {
                if (elements[i].Equals(obj) == true)
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveFirstOccurrence(object obj)
        {
            for (int i = head; i < tail + 1; i++)
            {
                if (elements[i].Equals(obj) == true)
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity > elements.Length)
            {
                int newCapacity = elements.Length * 2;
                E[] newElements = new E[newCapacity];
                for (int i = 0; i < elements.Length; i++)
                {
                    newElements[i] = elements[i];
                }
                elements = newElements;
            }
        }

        private void RemoveAt(int index)
        {
            int actualIndex = head + index;
            for (int i = index; i < tail - head - 1; i++)
            {
                int nextIndex = head + i + 1;
                elements[actualIndex] = elements[nextIndex];
                actualIndex = nextIndex;
            }
            elements[(head + (tail - head) - 1)] = default(E);
            tail = tail - 1;
        }
    }
}