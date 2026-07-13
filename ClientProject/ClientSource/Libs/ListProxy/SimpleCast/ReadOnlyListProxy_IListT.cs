using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class ReadOnlyListProxy_IListT<TSource, TResult> : IReadOnlyList<TResult>
  {
    public ReadOnlyListProxy_IListT(IList<TSource> source) => Source = source;
    private IList<TSource> Source;

    public int Count => Source.Count;

    public TResult this[int index] => (TResult)(object)Source[index];

    public IEnumerable<TResult> Enumerate()
    {
      foreach (TSource o in Source)
      {
        yield return (TResult)(object)o;
      }
    }
    IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => Enumerate().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Enumerate().GetEnumerator();
  }
}