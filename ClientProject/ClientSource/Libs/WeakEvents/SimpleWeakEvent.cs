using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public class SimpleWeakEvent
  {
    private ConditionalWeakTable<object, Action> Table = new();

    public void Add(object target, Action callback) => Table.Add(target, callback);
    public void Remove(object target) => Table.Remove(target);

    public void Raise()
    {
      foreach (var (target, action) in Table)
      {
        action.Invoke();
      }
    }
  }
}