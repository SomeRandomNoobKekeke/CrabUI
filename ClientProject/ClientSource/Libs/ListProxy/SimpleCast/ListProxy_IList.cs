using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{

  public class ListProxy_IList<TResult> : IList<TResult>
  {
    public ListProxy_IList(IList source) => Source = source;
    private IList Source;


    public TResult this[int i]
    {
      get => (TResult)Source[i];
      set => Source[i] = value;
    }
    public int Count => Source.Count;
    public bool IsReadOnly => false;

    public int IndexOf(TResult item) => Source.IndexOf(item);
    public void Insert(int index, TResult item) => Source.Insert(index, item);
    public void RemoveAt(int index) => Source.RemoveAt(index);
    public void Add(TResult item) => Source.Add(item);
    public void Clear() => Source.Clear();
    public bool Contains(TResult item) => Source.Contains(item);
    public void CopyTo(TResult[] array, int arrayIndex) => Source.CopyTo(array, arrayIndex);
    public bool Remove(TResult item)
    {
      Source.Remove(item);
      return true;
    }

    public IEnumerable<TResult> Enumerate()
    {
      foreach (object o in Source)
      {
        yield return (TResult)o;
      }
    }

    IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => Enumerate().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Enumerate().GetEnumerator();
  }
}