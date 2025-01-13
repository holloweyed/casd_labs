
using System;
using System.Xml;

public class MyLinkedList<T>
{
    private Node<T> first;
    private Node<T> last;
    private int size;

    private class Node<T>
    {
        public T Value;
        public Node<T> Next;
        public Node<T> Previous;

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }

    public MyLinkedList(T[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Add(a[i]);
        }
    }

    public void Add(T e)
    {
        Node<T> newNode = new Node<T>(e);

        if (size == 0)
        {
            first = newNode;
            last = newNode;
        }
        else
        {
            last.Next = newNode;
            newNode.Previous = last;
            last = newNode;
        }
        size++;
    }

    public void AddAll(T[] a)
    {
        foreach (T item in a)
        {
            Add(item);
        }
    }

    public void Clear()
    {
        Node<T> node = first;
        while (node != null)
        {
            Node<T> temp = node;
            node = node.Next;
            temp.Value = (T)(default);
            temp.Next = null;
            temp.Previous = null;
        }
        first = null;
        last = null;
        size = 0;
    }

    public bool Contains(object o)
    {
        Node<T> node = first;
        while (node != null)
        {
            if (Equals(node.Value, o)) return true;
            node = node.Next;
        }
        return false;
    }

    public bool ContainsAll(T[] a)
    {
        foreach (T item in a)
        {
            if (!Contains(item))
                return false;
        }
        return true;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public bool Remove(object o)
    {
        Node<T> node = first;
        bool flag = false;
        while (node != null)
        {
            if (Equals(node.Value, o))
            {
                Node<T> newNode = node.Next;
                if (IndexOf(o) == 0)
                {
                    first = newNode;
                    first.Previous = null;
                    flag = true;
                }
                else if (LastIndexOf(o) == size - 1)
                {
                    last = node.Previous;
                    last.Next = null;
                    flag = true;
                }

                else
                {
                    node.Previous.Next = node.Next;
                    node.Next.Previous = node.Previous;

                    node.Value = (T)(default);
                    node.Next = null;
                    node.Previous = null;
                    node = newNode;
                    flag = true;
                }

                node = node.Next;
            }
        }
        if (flag)
            return true;
        return false;
    }

    public void RemoveAll(T[] a)
    {
        foreach (T item in a)
        {
            Remove(item);
        }
    }

    public void RetainAll(T[] a)
    {
        Node<T> node = first;
        while (node != null)
        {
            if (!Contains(node.Value))
            {
                Remove(node.Value);
            }
        }
    }

    public int Size()
    {
        return size;
    }

    public T[] ToArray()
    {
        Node<T> node = first;
        T[] newArray = new T[size];
        for (int i = 0; i < size; i++)
        {
            newArray[i] = node.Value;
            node = node.Next;
        }
        return newArray;
    }

    public T[] ToArray(T[] a)
    {
        Node<T> node = first;
        if (a == null || a.Length < size)
        {
            return ToArray();
        }
        for (int i = 0; i < size; i++)
        {
            a[i] = node.Value;
            node = node.Next;
        }
        if (a.Length > size)
        {
            a[size] = default;
        }
        return a;
    }

    public void Add(int index, T e)
    {
        Node<T> newNode = new Node<T>(e);
        Node<T> node = first;
        if (index == 0)
        {
            first = newNode;
            first.Previous = null;
            first.Next = node;
            node.Previous = first;
        }
        else if (index == size - 1)
        {
            node = last;
            last = newNode;
            last.Next = null;
            last.Previous = node;
        }
        else
        {
            node = node.Next;
            for (int i = 1; i < size; i++)
            {
                if (i == index)
                {
                    newNode.Next = node;
                    newNode.Previous = node.Previous;
                    size++;
                }
                node = node.Next;
            }
        }
    }

    public void AddAll(int index, T[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Add(index + i, a[i]);
        }
    }


    public T Get(int index)
    {
        Node<T> node = first;
        for (int i = 0; i < size; i++)
        {
            if (i == index) { return node.Value; }
            node = node.Next;
        }
        return default(T);
    }

    public int IndexOf(object o)
    {
        Node<T> node = first;
        for (int index = 0; index < size; index++)
        {
            if (Equals(o, node.Value))
                return index;
            node = node.Next;
        }
        return -1;
    }

    public int LastIndexOf(object o)
    {
        Node<T> node = last;
        for (int index = size - 1; index != 0; index--)
        {
            if (Equals(o, node.Value))
                return index;
            node = node.Previous;
        }
        return -1;
    }

    public T Remove(int index)
    {
        T t = Get(index);
        Remove(t);
        return t;
    }

    public void Set(int index, T e)
    {
        Node<T> node = first;
        for (int i = 0; i < size; i++)
        {
            if (i == index)
            {
                node.Value = e;
                break;
            }
            node = node.Next;
        }
    }

    public MyLinkedList<T> SubList(int fromIndex, int toIndex)
    {
        MyLinkedList<T> newList = new MyLinkedList<T>();
        for (int i = fromIndex; i < toIndex; i++)
        {
            T t = Get(i);
            newList.Add(t);
        }
        return newList;
    }

    public T Element()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Deque is empty.");
        return first.Value;
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
        return IsEmpty() ? (T)(default) : Element();
    }

    public T Poll()
    {
        if (IsEmpty())
            return default(T);
        T item = Element();
        Remove(item);
        return item;
    }

    public void AddFirst(T obj)
    {
        Add(0, obj);
    }

    public void AddLast(T obj)
    {
        Add(obj);
    }

    public T GetFirst()
    {
        return Element();
    }

    public T GetLast()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Deque is empty.");
        return Get(size - 1);
    }

    public bool OfferFirst(T obj)
    {
        try
        {
            AddFirst(obj);
            return true;
        }
        catch { return false; }
    }

    public bool OfferLast(T obj)
    {
        return Offer(obj);
    }

    public T Pop()
    {
        return Poll();
    }

    public void Push(T obj)
    {
        AddFirst(obj);
    }

    public T PeekFirst()
    {
        return Peek();
    }

    public T PeekLast()
    {
        if (IsEmpty())
            return default(T);
        return GetLast();
    }

    public T PollFirst()
    {
        return Poll();
    }

    public T PollLast()
    {
        if (IsEmpty())
            return default(T);
        T item = Get(size - 1);
        Remove(size - 1);
        return item;
    }

    public T RemoveLast()
    {
        return PollLast();
    }

    public T RemoveFirst()
    {
        return Poll();
    }

    public bool RemoveLastOccurrence(object obj)
    {
        try
        {
            int index = LastIndexOf(obj);
            Remove(index);
            return true;
        }

        catch { return false; }
    }

    public bool RemoveFirstOccurrence(object obj)
    {
        try
        {
            int index = IndexOf(obj);
            Remove(index);
            return true;
        }

        catch { return false; }
    }
}