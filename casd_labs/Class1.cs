using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    public class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;

        public MyVector(int initialCapacity, int capIncrement)
        {
            elementData = new T[0];
            elementCount = initialCapacity;
            capacityIncrement = capIncrement;
        }

        public MyVector(int initialCapacity)
        {
            elementData = new T[0];
            elementCount = initialCapacity;
            capacityIncrement = 0;
        }

        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }

        public MyVector(T[] a)
        {
            elementData = new T[a.Length];
            for (int i = 0; i < a.Length; i++) elementData[i] = a[i];
            elementCount = a.Length;
            capacityIncrement = 0;

        }

        public void Add(T e)
        {
            if (elementCount == elementData.Length)
            {
                if (capacityIncrement == 0)
                {
                    T[] array = new T[(int)(elementCount * 2) + 1];
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    elementData = array;
                }
                else
                {
                    T[] array = new T[(int)(elementCount + capacityIncrement)];
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    elementData = array;
                }
            }
            elementData[elementCount] = e;
            elementCount++;
        }

        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }

        public void Clear()
        {
            elementData = new T[0];
            elementCount = 0;
            capacityIncrement = 0;
        }

        public bool Contains(object o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (elementData[i].Equals(o)) return true;
            }
            return false;
        }

        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!Contains(a[i])) return false;
            }
            return true;
        }
        public bool isEmpty()
        {
            if (elementCount == 0) return true;
            else return false;
        }

        public void Remove(object o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (elementData[i].Equals(o))
                {
                    for (int j = i; j < elementCount - 1; j++)
                    {
                        elementData[j] = elementData[j + 1];
                    }
                    elementCount--;
                }
            }
        }

        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Remove(a[i]);
        }

        public void RetainALl(T[] a)
        {
            for (int i = 0; i < a.Length; i++) elementData[i] = a[i];
            elementCount = a.Length;
        }

        public int Size()
        {
            return elementCount;
        }

        public T[] ToArray()
        {
            T[] a = new T[elementCount];
            for (int i = 0; i < elementCount; i++) a[i] = elementData[i];
            return a;
        }

        public void ToArray(ref T[] a)
        {
            if (a.Length <= elementCount && a.Length > 0)
            {
                T[] arr = new T[a.Length + elementCount];
                for (int i = 0; i < a.Length; i++) arr[i] = a[i];
                for (int i = a.Length; i < elementCount; i++) arr[i] = elementData[i];
                a = arr;

            }
            else
            {
                T[] arr = new T[elementCount];
                for (int i = 0; i < elementCount; i++) arr[i] = elementData[i];
                a = arr;
            }
        }

        public void Add(int index, T e)
        {
            if (elementCount == elementData.Length)
            {
                if (capacityIncrement == 0)
                {
                    T[] array = new T[(int)(elementCount * 2) + 1];
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    elementData = array;
                }
                else
                {
                    T[] array = new T[(int)(elementCount + capacityIncrement)];
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    elementData = array;
                }
            }

            for (int i = elementCount; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }

            elementData[index] = e;
            elementCount++;
        }

        public void AddAll(int index, T[] a)
        {
            foreach (T e in a) Add(index, e);
        }

        public T Get(int index)
        {
            return elementData[index];
        }

        public int IndexOf(object o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        public int LastIndexOf(T o)
        {
            for (int i = elementCount - 1; i > 0; i--)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        public T Remove(int index)
        {
            T remElem = elementData[index];
            for (int i = index; i < elementCount - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }
            elementCount--;
            return remElem;
        }

        public void Set(int index, T e) => elementData[index] = e;

        public MyVector<T> SubList(int fromindex, int toindex)
        {
            T[] sArr = new T[toindex - fromindex];
            for (int i = fromindex + 1; i < sArr.Length; i++)
            {
                sArr[i] = elementData[i];
            }
            MyVector<T> sub = new MyVector<T>(sArr);
            return sub;
        }

        public T FirstElement()
        {
            return elementData[0];
        }

        public T LastElement()
        {
            return elementData[elementCount - 1];
        }

        public void RemoveElementAt(int pos)
        {
            for (int i = pos; i < elementCount - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }
            elementCount--;
        }

        public void RemoveRange(int begin, int end)
        {
            for (int i = begin; i < end + 1; i++)
            {
                RemoveElementAt(i);
            }
        }
    }
}
