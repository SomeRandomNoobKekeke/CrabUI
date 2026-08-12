using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CrabUI
{
  public class CUIDebugNode : DebugNode
  {
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1> : DebugNode<T1>
  {
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2> : DebugNode<T1, T2>
  {
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2, T3> : DebugNode<T1, T2, T3>
  {
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2, T3, T4> : DebugNode<T1, T2, T3, T4>
  {
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }

  public class CUIDebugNode<T1, T2, T3, T4, T5> : DebugNode<T1, T2, T3, T4, T5>
  {
    public CUIDebugNode(string type) : base(type, CUICore.DebugHub) { }
  }
}