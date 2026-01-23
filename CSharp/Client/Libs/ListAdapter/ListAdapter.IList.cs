using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public partial class ListAdapter<T> : IList
  {
    object? IList.this[int index]
    {
      get => Source[index];
      set => Source[index] = value;
    }

    int IList.Add(object? value) => Source.Add(value);
    bool IList.Contains(object? value) => Source.Contains(value);
    void IList.Clear() => Source.Clear();
    bool IList.IsReadOnly => true;
    bool IList.IsFixedSize => Source.IsFixedSize;
    int IList.IndexOf(object? value) => Source.IndexOf(value);
    void IList.Insert(int index, object? value) => Source.Insert(index, value);
    void IList.Remove(object? value) => Source.Remove(value);
    void IList.RemoveAt(int index) => Source.RemoveAt(index);


    void ICollection.CopyTo(Array array, int index) => Source.CopyTo(array, index);
    int ICollection.Count => Count;
    object ICollection.SyncRoot => Source.SyncRoot;
    bool ICollection.IsSynchronized => Source.IsSynchronized;
  }
}