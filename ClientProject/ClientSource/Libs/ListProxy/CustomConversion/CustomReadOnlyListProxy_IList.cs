using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class CustomReadOnlyListProxy_IList<TResult> : IReadOnlyList<TResult>
  {
    public CustomReadOnlyListProxy_IList(IList source, Func<object, TResult> forward)
    {
      Source = source;
      Forward = forward;
    }
    private Func<object, TResult> Forward;
    private IList Source;

    public int Count => Source.Count;

    public TResult this[int index] => Forward(Source[index]);

    public IEnumerable<TResult> Enumerate()
    {
      foreach (object o in Source)
      {
        yield return Forward(o);
      }
    }
    IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => Enumerate().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Enumerate().GetEnumerator();
  }
}