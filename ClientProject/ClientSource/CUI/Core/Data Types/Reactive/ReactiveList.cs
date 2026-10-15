using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using CUILibs;
namespace CursedUI
{
  public class ReactiveList<T> : IList<T>, IList
  {
    public event Action Changed;

    private List<T> _List = new();

    public T this[int index]
    {
      get => _List[index];
      set
      {
        _List[index] = value;
        Changed?.Invoke();
      }
    }

    object? IList.this[int index]
    {
      get => _List[index];
      set
      {
        _List[index] = (T)value;
        Changed?.Invoke();
      }
    }

    public int Count => _List.Count;
    public bool IsReadOnly => ((IList)_List).IsReadOnly;
    public bool IsFixedSize => ((IList)_List).IsFixedSize;
    public bool IsSynchronized => ((IList)_List).IsSynchronized;
    public object SyncRoot => ((IList)_List).SyncRoot;

    public void Add(T item)
    {
      _List.Add(item);
      Changed?.Invoke();
    }

    public int Add(object? value)
    {
      _List.Add((T)value);
      Changed?.Invoke();
      return _List.Count - 1;
    }

    public void Clear()
    {
      _List.Clear();
      Changed?.Invoke();
    }

    public bool Contains(T item) => _List.Contains(item);

    public bool Contains(object? value) => _List.Contains((T)value);


    public void CopyTo(T[] array, int arrayIndex) => _List.CopyTo(array, arrayIndex);
    public void CopyTo(Array array, int index) => ((IList)_List).CopyTo(array, index);

    public IEnumerator<T> GetEnumerator() => _List.GetEnumerator();

    public int IndexOf(T item) => _List.IndexOf(item);

    public int IndexOf(object? value) => _List.IndexOf((T)value);

    public void Insert(int index, T item)
    {
      _List.Insert(index, item);
      Changed?.Invoke();
    }

    public void Insert(int index, object? value)
    {
      _List.Insert(index, (T)value);
      Changed?.Invoke();
    }

    public bool Remove(T item)
    {
      bool result = _List.Remove(item);
      Changed?.Invoke();
      return result;
    }

    public void Remove(object? value)
    {
      _List.Remove((T)value);
      Changed?.Invoke();
    }

    public void RemoveAt(int index)
    {
      _List.RemoveAt(index);
      Changed?.Invoke();
    }

    IEnumerator IEnumerable.GetEnumerator() => _List.GetEnumerator();
  }
}