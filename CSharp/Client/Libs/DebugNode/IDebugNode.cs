using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// All IDebugNode are derived from ClearableEventBase
  /// This could be an abstract class if double inheritance was a thing
  /// </summary>
  public interface IDebugNode : IClearableEvent
  {
    public string Name { get; set; }
    public ClearableEvent<DebugEvent> Pin { get; }


    public void Send() { }
    public void Send(object arg1) { }
    public void Send(object arg1, object arg2) { }
    public void Send(object arg1, object arg2, object arg3) { }
    public void Send(object arg1, object arg2, object arg3, object arg4) { }
    public void Send(object arg1, object arg2, object arg3, object arg4, object arg5) { }
  }
}