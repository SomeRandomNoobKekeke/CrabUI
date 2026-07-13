using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class ListProxy_IListT<TSource, TResult> : IList<TResult>
  {
    public ListProxy_IListT(IList<TSource> source) => Source = source;
    private IList<TSource> Source;

    public int Count => Source.Count;
    public bool IsReadOnly => false;

    public TResult this[int i]
    {
      get => (TResult)(object)Source[i];
      set => Source[i] = (TSource)(object)value;
    }

    public int IndexOf(TResult item) => Source.IndexOf((TSource)(object)item);
    public void Insert(int index, TResult item) => Source.Insert(index, (TSource)(object)item);
    public void RemoveAt(int index) => Source.RemoveAt(index);
    public void Add(TResult item) => Source.Add((TSource)(object)item);
    public void Clear() => Source.Clear();
    public bool Contains(TResult item) => Source.Contains((TSource)(object)item);

    public void CopyTo(TResult[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public bool Remove(TResult item) => Source.Remove((TSource)(object)item);

    public IEnumerable<TResult> Enumerate()
    {
      foreach (TSource o in Source)
      {
        yield return (TResult)(object)o;
      }
    }
    public IEnumerator<TResult> GetEnumerator() => Enumerate().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Enumerate().GetEnumerator();
  }
}