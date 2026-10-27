using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CursedUI
{
  // https://gist.github.com/cocowalla/5d181b82b9a986c6761585000901d1b8
  public class Debouncer
  {
    public ConcurrentBag<Action> Pending = [];

    private int counter;

    public void Debouce(Action action, int delay)
    {
      var current = Interlocked.Increment(ref counter);

      Task.Delay(delay).ContinueWith(task =>
      {
        if (current == counter) Pending.Add(action);
      });
    }

    public void Update()
    {
      if (Pending.IsEmpty) return;

      foreach (Action action in Pending)
      {
        action();
      }

      Pending.Clear();
    }
  }
}