using System;
using System.Collections;
using System.Collections.Generic;

//ref link:https://www.youtube.com/watch?v=2MPDSWuZOEI&list=PLRwVmtr-pp07QlmssL4igw1rnrttJXerL&index=20
//ctrl+shift+space --- check target details 
// list -- are dynamic, can grow and shrink
// list -- manage array underneath
// all link function rely on IEnumerator
// IEnumerable -- the container sequence just like LINQ while IEnumerator --- can walk through the sequence of both linq and IEnumrable
// Indexer -- knowledge in operator overloading

class MeList<T> : IEnumerable<T>
{
    //T[] items = new T[5];
    T[] items;
    //int count;
    public MeList(int capacity = 5)
    {
        items = new T[capacity];
    }
    public void Add(T item)
    {
        if (Count == items.Length)
            Array.Resize(ref items, items.Length * 2);  // resize the underlying containers --- add slots by x2 of previous slot
        items[Count++] = item;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return items[i];      // requires yield return knowledge
        //return new MeEnumerator(this);
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
    public T this[int index]    //indexer -- looks like property
    //public T this[int index, string blah, char c]
    {
        get
        {
            //if (index >= count || index < 0)              highlight+ctrl+. + extract method 
            //    throw new IndexOutOfRangeException();
            CheckBoundaries(index);
            return items[index];
        }
        set
        {
            //if (index >= count || index < 0)
            //    throw new IndexOutOfRangeException();
            CheckBoundaries(index);
            items[index] = value;
        }
    }

    void CheckBoundaries(int index)
    {
        if (index >= Count || index < 0)
            throw new IndexOutOfRangeException();
    }
    public int Capacity
    {
        get { return items.Length; }
    }
    //public int Count { get { return count; } }
    public int Count { get; private set; }
    public void Clear()     // for data waste not cleaned
    {   
        /*      Optimization
        Count = 0;
        if (typeof(T).BaseType.Equals(typeof(ValueType)))   // no worry garbage collection
            return;*/
        
        // nullifying all items(value/reference) types
        for (int i = 0; i < Count; i++)
            items[i] = default(T);
        Count = 0;
    }
    public void TrimExcess()    // for MeList<int>
    {
        T[] newArray = new T[Count];
        Array.Copy(items, newArray, Count);
        items = newArray;
    }
}

class MainClass
{
    static void Main()
    {
        // Capacity, Count, TrimExcess, Clear
        //List<int> myPartyAges = new List<int>() { 25, 34, 32 };
        //List<int> myPartyAges = new List<int>(10) { 25, 34, 32 };
        //Console.WriteLine(myPartyAges.Count);
        //Console.WriteLine(myPartyAges.Capacity);
        //myPartyAges.Add(99);
        //myPartyAges.Add(101);
        //Console.WriteLine(myPartyAges.Count);
        //Console.WriteLine(myPartyAges.Capacity);
        //List<int> myPartyAges = new List<int>() { };
        //List<int> myPartyAges = new List<int>(6000) { };
        MeList<int> myPartyAges = new MeList<int>(6000) { };
        //MeList<int> myPartyAges = new MeList<int>() { };
        int currentCapacity = myPartyAges.Capacity;
        Console.WriteLine(currentCapacity);
        for (int i = 0; i < 500; i++)
        {
            myPartyAges.Add(i);
            //if(currentCapacity != myPartyAges.Capacity)
            //{
            //    Console.WriteLine("Resized to " + myPartyAges.Capacity);
            //    currentCapacity = myPartyAges.Capacity;
            //}
        }
        Console.WriteLine(myPartyAges.Capacity);
        myPartyAges.TrimExcess();   // remove excess byte(int)
        Console.WriteLine(myPartyAges.Capacity);
        myPartyAges.Clear();    // remove all items on the array
        Console.WriteLine(myPartyAges.Capacity);
    }
}