using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CrabUI
{
  public class CUIDebugger : CUIDefault.Frame
  {
    public DebugHub DebugHub => CUICore.DebugHub;

    public ClearableEvent<DebugEvent> Input { get; } = new();


    public CUIDebugger() : base("Debug")
    {
      TargetMainComponent = CUI.TopMain;

      Absolute = new CUINullRect(w: 400, h: 600);
      Anchor = CUIAnchor.LeftCenter;

      OnOpen += () => DebugHub.Output.Map(Input);
      OnClose += () => DebugHub.Output.Unmap(Input);
    }
  }
}