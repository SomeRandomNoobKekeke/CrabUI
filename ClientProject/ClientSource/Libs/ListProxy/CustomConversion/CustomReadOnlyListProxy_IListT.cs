using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{

  public class CustomReadOnlyListProxy_IListT<TSource, TResult> : IReadOnlyList<TResult>
  {
    public CustomReadOnlyListProxy_IListT(IList<TSource> source, Func<TSource, TResult> forward)
    {
      Source = source;
      Forward = forward;
    }
    private Func<TSource, TResult> Forward;
    private IList<TSource> Source;

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