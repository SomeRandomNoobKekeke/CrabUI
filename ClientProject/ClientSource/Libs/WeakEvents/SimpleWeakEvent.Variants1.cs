using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public class SimpleWeakEvent<T1>
  {
    private ConditionalWeakTable<object, Action<T1>> Table = new();

    public void Add(object target, Action<T1> callback) => Table.Add(target, callback);
    public void Remove(object target) => Table.Remove(target);

    public void Raise(T1 arg1)
    {
      foreach (var (target, action) in Table)
      {
        action.Invoke(arg1);
      }
    }
  }
}