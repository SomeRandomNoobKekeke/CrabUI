using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class CustomListProxy_IReadOnlyListT<TSource, TResult> : IReadOnlyList<TResult>
  {
    public CustomListProxy_IReadOnlyListT(IReadOnlyList<TSource> source, Func<TSource, TResult> forward)
    {
      Source = source;
      Forward = forward;
    }
    private Func<TSource, TResult> Forward;
    private IReadOnlyList<TSource> Source;

    public int Count => Source.Count;

    public TResult this[int index] => Forward(Source[index]);

    public IEnumerable<TResult> Enumerate()
    {
      foreach (TSource o in Source)
      {
        yield return Forward(o);
      }
    }
    IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => Enumerate().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Enumerate().GetEnumerator();
  }
}