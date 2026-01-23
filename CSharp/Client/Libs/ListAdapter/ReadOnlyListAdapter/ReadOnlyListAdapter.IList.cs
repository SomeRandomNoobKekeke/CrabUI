using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public partial class ReadOnlyListAdapter<T> : IList
  {
    object? IList.this[int index]
    {
      get => Source[index];
      set => throw new NotSupportedException("this[int index] = value not supported in ReadOnlyListAdapter");
    }

    int IList.Add(object? value) => throw new NotSupportedException("Add(object? value) not supported in ReadOnlyListAdapter");
    bool IList.Contains(object? value) => Source.Contains(value);
    void IList.Clear() => throw new NotSupportedException("Clear() not supported in ReadOnlyListAdapter");
    bool IList.IsReadOnly => true;
    bool IList.IsFixedSize => Source.IsFixedSize;
    int IList.IndexOf(object? value) => Source.IndexOf(value);
    void IList.Insert(int index, object? value) => throw new NotSupportedException("Insert(int index, object? value) not supported in ReadOnlyListAdapter");
    void IList.Remove(object? value) => throw new NotSupportedException("Remove(object? value) not supported in ReadOnlyListAdapter");
    void IList.RemoveAt(int index) => throw new NotSupportedException("RemoveAt(int index) not supported in ReadOnlyListAdapter");


    void ICollection.CopyTo(Array array, int index) => Source.CopyTo(array, index);
    int ICollection.Count => Count;
    object ICollection.SyncRoot => Source.SyncRoot;
    bool ICollection.IsSynchronized => Source.IsSynchronized;
  }
}