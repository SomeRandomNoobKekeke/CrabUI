using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  /// <summary>
  /// not used
  /// </summary>
  public class ActionInvoker
  {
    private event Action Action;
    public void Invoke() => Action?.Invoke();
    public void Subscribe(Action callback) => Action += callback;
    public void Unsubscribe(Action callback) => Action -= callback;
  }
}