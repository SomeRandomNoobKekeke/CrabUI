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
  public class Debouncer
  {
    private ConcurrentBag<Action> Pending = [];
    private ConcurrentDictionary<MethodInfo, CancellationTokenSource> CancelationTokens = [];

    public void Debounce(Action action, int delay)
    {
      if (CancelationTokens.TryGetValue(action.Method, out var cts))
      {
        cts.Cancel();
      }

      CancelationTokens[action.Method] = new CancellationTokenSource();

      Task.Delay(delay, CancelationTokens[action.Method].Token)
        .ContinueWith(t =>
        {
          if (t.IsCompletedSuccessfully)
          {
            Pending.Add(action);
            CancelationTokens.Remove(action.Method, out var _);
          }
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