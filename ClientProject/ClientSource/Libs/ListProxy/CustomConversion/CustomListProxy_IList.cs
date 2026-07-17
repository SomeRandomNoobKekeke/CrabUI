using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{

  public class CustomListProxy_IList<TResult> : IList<TResult>
  {
    public CustomListProxy_IList(IList source, Func<object, TResult> forward, Func<TResult, object> backward = null)
    {
      Source = source;
      Forward = forward;
      Backward = backward is null ? o => o : backward;
    }

    private IList Source;
    private Func<object, TResult> Forward;
    private Func<TResult, object> Backward;

    public TResult this[int i]
    {
      get => Forward(Source[i]);
      set => Source[i] = Backward(value);
    }
    public int Count => Source.Count;
    public bool IsReadOnly => false;

    public int IndexOf(TResult item) => Source.IndexOf(Backward(item));
    public void Insert(int index, TResult item) => Source.Insert(index, Backward(item));
    public void RemoveAt(int index) => Source.RemoveAt(index);
    public void Add(TResult item) => Source.Add(Backward(item));
    public void Clear() => Source.Clear();
    public bool Contains(TResult item) => Source.Contains(Backward(item));
    public void CopyTo(TResult[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }
    public bool Remove(TResult item)
    {
      Source.Remove(Backward(item));
      return true;
    }

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