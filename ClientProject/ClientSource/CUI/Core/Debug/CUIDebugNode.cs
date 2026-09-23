using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CursedUI
{
  public class CUIDebugNode : DebugNode
  {
    public void Send(CUIVisualComponent component)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new CUIDebugEvent()
        {
          RelatedComponent = component,
          Type = Type,
          Args = [],
          Msg = MsgFactory.Invoke(),
        });
      }
    }
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1> : DebugNode<T1>
  {
    public void Send(CUIVisualComponent component, T1 arg1)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new CUIDebugEvent()
        {
          RelatedComponent = component,
          Type = Type,
          Args = [arg1],
          Msg = MsgFactory.Invoke(arg1),
        });
      }
    }
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2> : DebugNode<T1, T2>
  {
    public void Send(CUIVisualComponent component, T1 arg1, T2 arg2)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new CUIDebugEvent()
        {
          RelatedComponent = component,
          Type = Type,
          Args = [arg1, arg2],
          Msg = MsgFactory.Invoke(arg1, arg2),
        });
      }
    }
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2, T3> : DebugNode<T1, T2, T3>
  {
    public void Send(CUIVisualComponent component, T1 arg1, T2 arg2, T3 arg3)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new CUIDebugEvent()
        {
          RelatedComponent = component,
          Type = Type,
          Args = [arg1, arg2, arg3],
          Msg = MsgFactory.Invoke(arg1, arg2, arg3),
        });
      }
    }
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2, T3, T4> : DebugNode<T1, T2, T3, T4>
  {
    public void Send(CUIVisualComponent component, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new CUIDebugEvent()
        {
          RelatedComponent = component,
          Type = Type,
          Args = [arg1, arg2, arg3, arg4],
          Msg = MsgFactory.Invoke(arg1, arg2, arg3, arg4),
        });
      }
    }
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2, T3, T4, T5> : DebugNode<T1, T2, T3, T4, T5>
  {
    public void Send(CUIVisualComponent component, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new CUIDebugEvent()
        {
          RelatedComponent = component,
          Type = Type,
          Args = [arg1, arg2, arg3, arg4, arg5],
          Msg = MsgFactory.Invoke(arg1, arg2, arg3, arg4, arg5),
        });
      }
    }
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }
}