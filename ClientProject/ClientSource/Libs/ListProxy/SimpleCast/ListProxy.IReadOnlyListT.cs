using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class ListProxy_IReadOnlyListT<TSource, TResult> : IReadOnlyList<TResult>
  {
    public ListProxy_IReadOnlyListT(IReadOnlyList<TSource> source) => Source = source;
    private IReadOnlyList<TSource> Source;

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