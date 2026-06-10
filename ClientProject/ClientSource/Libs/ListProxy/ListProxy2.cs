using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ListProxy<TSource, TResult> : IReadOnlyList<TResult>, IList
  {
    public const string ReadOnlyExceptionText = "it's supposed to be readonly lol";
    public const string TooLazyExceptionText = "too lazy to implement, sry";

    public struct ProxyEnumerator : IEnumerator<TResult>, IEnumerator
    {
      private IEnumerator<TSource> Enumerator;
      private Func<TSource, TResult> Transform;
      public ProxyEnumerator(IEnumerator<TSource> enumerator, Func<TSource, TResult> transform)
      {
        Enumerator = enumerator;
        Transform = transform;
      }

      public bool MoveNext() => Enumerator.MoveNext();
      public TResult Current => Transform(Enumerator.Current);
      object? IEnumerator.Current => Transform(Enumerator.Current);
      void IEnumerator.Reset() => Enumerator.Reset();
      public void Dispose() { }
    }


    public TResult this[int i] => Transform(Source[i]);
    public int Count => Source.Count;

    public ProxyEnumerator GetEnumerator() => new ProxyEnumerator(Source.GetEnumerator(), Transform);

    IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TResult>)this).GetEnumerator();


    #region IList
    object? IList.this[int i]
    {
      get => Transform(Source[i]);
      set => throw new NotSupportedException(ReadOnlyExceptionText);
    }
    int IList.Add(object? value) => throw new NotSupportedException(ReadOnlyExceptionText);
    bool IList.Contains(object? value) => throw new NotImplementedException(TooLazyExceptionText);
    void IList.Clear() => throw new NotSupportedException(ReadOnlyExceptionText);
    bool IList.IsReadOnly => true;
    bool IList.IsFixedSize => true;
    int IList.IndexOf(object? value) => throw new NotImplementedException(TooLazyExceptionText);
    void IList.Insert(int index, object? value) => throw new NotSupportedException(ReadOnlyExceptionText);
    void IList.Remove(object? value) => throw new NotSupportedException(ReadOnlyExceptionText);
    void IList.RemoveAt(int index) => throw new NotSupportedException(ReadOnlyExceptionText);
    #endregion

    #region ICollection
    void ICollection.CopyTo(Array array, int index) => throw new NotImplementedException(TooLazyExceptionText);
    int ICollection.Count => Source.Count;
    object ICollection.SyncRoot => (Source as ICollection).SyncRoot;
    bool ICollection.IsSynchronized => (Source as ICollection)?.IsSynchronized ?? false;
    #endregion




    private IReadOnlyList<TSource> Source;
    private Func<TSource, TResult> Transform;

    //TODO Add
    // public ListProxy(IList<TSource> source, Func<TSource, TResult> transform)
    // {
    //   ArgumentNullException.ThrowIfNull(source);
    //   ArgumentNullException.ThrowIfNull(transform);

    //   Source = source.AsReadOnly(); //TODO don't create new wrapper here
    //   Transform = transform;
    // }

    public ListProxy(IReadOnlyList<TSource> source, Func<TSource, TResult> transform)
    {
      ArgumentNullException.ThrowIfNull(source);
      ArgumentNullException.ThrowIfNull(transform);

      Source = source;
      Transform = transform;
    }
  }
}