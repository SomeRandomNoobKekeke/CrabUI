using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class ReadOnlyListProxy_IList<TResult> : IReadOnlyList<TResult>
  {
    public ReadOnlyListProxy_IList(IList source) => Source = source;
    private IList Source;

    public int Count => Source.Count;

    public TResult this[int index] => (TResult)Source[index];

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