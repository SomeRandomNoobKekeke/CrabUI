using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public partial class ReadOnlyListAdapter<T>
  {
    public struct ProxyEnumerator : IEnumerator<T>, IEnumerator
    {
      private IEnumerator Enumerator;
      public ProxyEnumerator(IEnumerator enumerator) => Enumerator = enumerator;
      public bool MoveNext() => Enumerator.MoveNext();
      public T Current => (T)Enumerator.Current;
      object? IEnumerator.Current => Enumerator.Current;
      void IEnumerator.Reset() => Enumerator.Reset();
      public void Dispose() { }
    }
  }
}