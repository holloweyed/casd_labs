using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    public class Heap<T> where T : IComparable<T>
    {
        private T[] elements;
        private int size;
        private readonly bool isMaxHeap;

        public Heap(T[] array, bool isMaxHeap = true)
        {
            elements = new T[size];
            Array.Copy(array, elements, size);
            CreateHeap();
        }

        private void CreateHeap()
        {
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public T GetTop()
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty.");
            return elements[0];
        }

        public T PopTop()
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty.");
            T root = elements[0];
            elements[0] = elements[size - 1];
            size--;
            Heapify(0);
            return root;
        }

        public void IncreaseKey(int index, T newKey)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));

            if ((isMaxHeap && newKey.CompareTo(elements[index]) < 0) ||
                (!isMaxHeap && newKey.CompareTo(elements[index]) > 0))
                throw new ArgumentException("New value is not applicable.");

            elements[index] = newKey;

            while (index > 0 && Compare(elements[Parent(index)], elements[index]) > 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        public void Add(T value)
        {
            if (size == elements.Length)
            {
                Array.Resize(ref elements, size * 2);
            }

            elements[size] = value;
            size++;
            int index = size - 1;

            while (index > 0 && Compare(elements[Parent(index)], elements[index]) > 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        public Heap<T> Merge(Heap<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            T[] mergedArray = new T[size + other.size];
            Array.Copy(elements, mergedArray, size);
            Array.Copy(other.elements, 0, mergedArray, size, other.size);
            return new Heap<T>(mergedArray, isMaxHeap);
        }

        private void Heapify(int index)
        {
            int left = LeftChild(index);
            int right = RightChild(index);
            int extreme = index;

            if (left < size && Compare(elements[left], elements[extreme]) < 0)
                extreme = left;

            if (right < size && Compare(elements[right], elements[extreme]) < 0)
                extreme = right;

            if (extreme != index)
            {
                Swap(index, extreme);
                Heapify(extreme);
            }
        }

        private int Parent(int index) => (index - 1) / 2;
        private int LeftChild(int index) => 2 * index + 1;
        private int RightChild(int index) => 2 * index + 2;

        private void Swap(int i, int j)
        {
            T temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }

        private int Compare(T x, T y)
        {
            return isMaxHeap ? x.CompareTo(y) : y.CompareTo(x);
        }
    }
}
