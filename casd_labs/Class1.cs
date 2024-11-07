using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    internal class Class1
    {
        public class MyArrayList<T>
        {
            private T[] elementData;
            private int size;

            public MyArrayList()
            {
                elementData = new T[0];
                size = 0;
            }

            public MyArrayList(T[] a)
            {
                elementData = a;
                size = a.Length;
            }

            public MyArrayList(int capacity)
            {
                elementData = new T[capacity];
                size = 0;
            }

            public void Add(T e)
            {
                if (size == elementData.Length)
                {
                    T[] a = new T[(int)(size * 1.5 + 1)];
                    for (int i = 0; i < size; i++) a[i] = elementData[i];
                    elementData = a;
                }

                elementData[size] = e;
            }

            public void AddAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++) Add(a[i]);
            }

            public void Clear()
            {
                elementData = null;
                size = 0;
            }

            public bool Contains(T o)
            {
                if (!(elementData == null))
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (elementData[i].Equals(o)) return true;
                    }
                }
                return false;
            }

            public bool ContainsAll(T[] a)
            {
                int f = 1;
                for (int i = 0; i < a.Length; i++)
                {
                    if (!Contains(a[i])) f = 0;
                }
                if (f == 0) return false;
                return true;
            }

            public bool IsEmpty()
            {
                if (size == 0) return true;
                else return false;
            }

            public void Remove(T o)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elementData[i].Equals(o))
                    {
                        for (int j = i; j < size - 1; j++)
                        {
                            elementData[j] = elementData[j + 1];
                        }
                        size--;
                    }
                }
            }

            public void RemoveAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++) Remove(a[i]);
            }

            public void RetainAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++) elementData[i] = a[i];
                size = a.Length;
            }

            public int Size()
            {
                return size;
            }

            public T[] ToArray()
            {
                T[] a = new T[size];
                for (int i = 0; i < size; i++) a[i] = elementData[i];
                return a;
            }

            public void ToArray(ref T[] a)
            {
                if (a.Length <= size && a.Length > 0)
                {
                    T[] arr = new T[a.Length + size];
                    for (int i = 0; i < a.Length; i++) arr[i] = a[i];
                    for (int i = a.Length; i < size; i++) arr[i] = elementData[i];
                    a = arr;

                }
                else
                {
                    T[] arr = new T[size];
                    for (int i = 0; i < size; i++) arr[i] = elementData[i];
                    a = arr;
                }
            }

            public void Add(int index, T e)
            {
                if (size == elementData.Length)
                {
                    T[] a = new T[(int)(size * 1.5 + 1)];
                    for (int i = 0; i < size; i++) a[i] = elementData[i];
                    elementData = a;
                }
                for (int i = size; i > index; i--)
                {
                    elementData[i] = elementData[i - 1];
                }

                elementData[index] = e;
                size++;
            }

            public void AddAll(int index, T[] a)
            {
                foreach (T e in a) Add(index, e);
            }

            public T Get(int index)
            {
                return elementData[index];
            }

            public int IndexOf(T o)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elementData[i].Equals(o)) return i;
                }
                return -1;
            }

            public int LastIndexOf(T o)
            {
                for (int i = size - 1; i > 0; i--)
                {
                    if (elementData[i].Equals(o)) return i;
                }
                return -1;
            }

            public T Remove(int index)
            {
                T remElem = elementData[index];
                for (int i = index; i < size - 1; i++)
                {
                    elementData[i] = elementData[i + 1];
                }
                size--;
                return remElem;
            }

            public void Set(int index, T e)
            {
                elementData[index] = e;
            }

            public MyArrayList<T> SubList(int fromindex, int toindex)
            {
                T[] sArr = new T[toindex - fromindex];
                for (int i = fromindex + 1; i < sArr.Length; i++)
                {
                    sArr[i] = elementData[i];
                }
                MyArrayList<T> sub = new MyArrayList<T>(sArr);
                return sub;
            }
        }
    }
}
