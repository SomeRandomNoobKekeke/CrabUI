using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// Provides access to List<Derrived> as List<Base> without creating a new list
  /// </summary>
  public class ListProxy<T> : IReadOnlyList<T>
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


    public T this[int i]
    {
      get
      {
        T result = default;
        try
        {
          result = (T)Source[i];
        }
        catch (Exception e)
        {
          Logger.Default.Error($"Source: [{Source}]");
          Logger.Default.Error($"Source[i]: [{Source[i]}]");
          throw;
        }
        return result;
      }
    }
    public int Count => Source.Count;

    public ProxyEnumerator GetEnumerator() => new ProxyEnumerator(Source.GetEnumerator());

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();


    private IList Source;
    public ListProxy(IList source) => Source = source;
  }
}