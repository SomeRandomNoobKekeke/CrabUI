using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// Provides access to IList<A> as IList<B>, not type safe
  /// </summary>
  public partial class ListAdapter<T> : ListAdapterBase, IList<T>
  {
    public T this[int index]
    {
      get => (T)Source[index];
      set => Source[index] = value;
    }
    public int IndexOf(T item) => Source.IndexOf(item);
    public void Insert(int index, T item) => Source.Insert(index, item);
    public void RemoveAt(int index) => Source.RemoveAt(index);

    public int Count => Source.Count;
    public bool IsReadOnly => Source.IsReadOnly;
    public void Add(T item) => Source.Add(item);
    public void Clear() => Source.Clear();
    public bool Contains(T item) => Source.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => Source.CopyTo(array, arrayIndex);
    public bool Remove(T item)
    {
      Source.Remove(item);
      return true;
    }

    public ProxyEnumerator GetEnumerator() => new ProxyEnumerator(Source.GetEnumerator());
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();


    public ListAdapter(IList source)
    {
      if (source is ListAdapterBase)
      {
        Source = ((ListAdapterBase)source).Source;
      }
      else
      {
        Source = source;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj is null || obj is not ListAdapter<T> other) return false;
      return Source == other.Source;
    }
  }
}