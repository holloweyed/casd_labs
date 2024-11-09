using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class MyPriorityQueue<T>
{
    private T[] queue;
    private int size;
    private IComparer<T> comparator;

    public MyPriorityQueue() : this(11, null) { }

    public MyPriorityQueue(T[] a)
    {
        queue = new T[a.Length];
        Array.Copy(a, queue, a.Length);
        size = a.Length;
        comparator = Comparer<T>.Default;
        CreateHeap();
    }

    public MyPriorityQueue(int initialCapacity) : this(initialCapacity, null) { }

    public MyPriorityQueue(int initialCapacity, IComparer<T> comparator)
    {
        queue = new T[initialCapacity];
        size = 0;
        comparator = comparator ?? Comparer<T>.Default;
    }

    public MyPriorityQueue(MyPriorityQueue<T> c)
    {
        queue = new T[c.size];
        Array.Copy(c.queue, queue, c.size);
        size = c.size;
        comparator = c.comparator;
        CreateHeap();
    }

    private void CreateHeap()
    {
        for (int i = size / 2 - 1; i >= 0; i--)
        {
            Heapify(i);
        }
    }

    private void Heapify(int index)
    {
        int left = LeftChild(index);
        int right = RightChild(index);
        int extreme = index;

        if (left < size && Compare(queue[left], queue[extreme]) < 0)
            extreme = left;
        if (right < size && Compare(queue[right], queue[extreme]) < 0)
            extreme = right;

        if (extreme != index)
        {
            Swap(index, extreme);
            Heapify(extreme);
        }
    }

    private int LeftChild(int index) => 2 * index + 1;
    private int RightChild(int index) => 2 * index + 2;

    private void Swap(int i, int j)
    {
        T temp = queue[i];
        queue[i] = queue[j];
        queue[j] = temp;
    }

    private int Compare(T x, T y) => comparator.Compare(x, y);

    public void Add(T e)
    {
        if (size >= queue.Length)
        {
            int newCapacity = queue.Length < 64 ? queue.Length + 2 : (int)(queue.Length * 1.5);
            Array.Resize(ref queue, newCapacity);
        }

        queue[size] = e;
        size++;
        HeapifyUp(size - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (Compare(queue[parent], queue[index]) <= 0) break;
            Swap(parent, index);
            index = parent;
        }
    }

    public void AddAll(T[] a)
    {
        foreach (var item in a)
        {
            Add(item);
        }
    }

    public void Clear()
    {
        queue = new T[queue.Length];
        size = 0;
    }

    public bool Contains(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (queue[i].Equals(o))
                return true;
        }
        return false;
    }

    public bool ContainsAll(T[] a)
    {
        foreach (var item in a)
        {
            if (!Contains(item)) return false;
        }
        return true;
    }

    public bool IsEmpty() => size == 0;

    public void Remove(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (queue[i].Equals(o))
            {
                if (i < 0 || i >= size)
                    throw new ArgumentOutOfRangeException(nameof(i));

                queue[i] = queue[size - 1];
                size--;
                Heapify(i);
            }
        }
    }

    public void RemoveAll(T[] a)
    {
        foreach (var item in a)
        {
            Remove(item);
        }
    }

    public void RetainAll(T[] a)
    {
        HashSet<T> toRetain = new HashSet<T>(a);
        for (int i = 0; i < size; i++)
        {
            if (!toRetain.Contains(queue[i]))
            {
                if (i < 0 || i >= size)
                    throw new ArgumentOutOfRangeException(nameof(i));

                queue[i] = queue[size - 1];
                size--;
                Heapify(i);
                i--;
            }
        }
    }

    public int Size() => size;

    public T[] ToArray()
    {
        T[] result = new T[size];
        Array.Copy(queue, result, size);
        return result;
    }

    public T[] ToArray(T[] a)
    {
        if (a == null || a.Length < size)
            a = new T[size];
        Array.Copy(queue, a, size);
        return a;
    }

    public T Element()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Priority queue is empty.");
        return queue[0];
    }

    public bool Offer(T obj)
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

    public T Peek()
    {
        if (IsEmpty())
            return default(T);
        return queue[0];
    }

    public T Poll()
    {
        if (IsEmpty())
            return default(T);
        T result = queue[0];
        queue[0] = queue[size - 1];
        size--;
        Heapify(0);
        return result;
    }
}