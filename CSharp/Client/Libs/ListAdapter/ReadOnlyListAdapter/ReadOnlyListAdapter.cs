using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// Provides readonly access to IList<A> as IList<B>
  /// </summary>
  public partial class ReadOnlyListAdapter<T> : ListAdapterBase, IReadOnlyList<T>
  {
    public T this[int index]
    {
      get => (T)Source[index];
    }
    public int IndexOf(T item) => Source.IndexOf(item);
    public int Count => Source.Count;
    public bool IsReadOnly => true;
    public bool Contains(T item) => Source.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => Source.CopyTo(array, arrayIndex);

    public ProxyEnumerator GetEnumerator() => new ProxyEnumerator(Source.GetEnumerator());
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

    public ReadOnlyListAdapter(IList source)
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
      if (obj is null || obj is not ReadOnlyListAdapter<T> other) return false;
      return Source == other.Source;
    }
  }
}