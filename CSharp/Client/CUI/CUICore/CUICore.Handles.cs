using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUICore
  {
    public class UpdateConnectionHandle : Part
    {
      public void Update() => Self.LifeCycle.Update();
    }

    public class DrawBeforeGUIConnectionHandle : Part
    {
      public void Draw() => Self.LifeCycle.DrawBeforeGUI();
    }

    public class DrawAfterGUIConnectionHandle : Part
    {
      public void Draw() => Self.LifeCycle.DrawAfterGUI();
    }

    public UpdateConnectionHandle UpdateHandle { get; } = new();
    public DrawBeforeGUIConnectionHandle DrawBeforeGUIHandle { get; } = new();
    public DrawAfterGUIConnectionHandle DrawAfterGUIHandle { get; } = new();
  }
}