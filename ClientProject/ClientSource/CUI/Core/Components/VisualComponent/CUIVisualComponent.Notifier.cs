using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CrabUI
{

  //CRINGE but at least it's fast and not scattered all over the place
  public partial class CUIVisualComponent
  {
    protected LayoutUpdateNotifier_Part LayoutUpdateNotifier { get; } = new();
    protected VisualRestructureNotifier_Part VisualRestructureNotifier { get; } = new();

    public class LayoutUpdateNotifier_Part : Part
    {
      public void Notify()
      {
        if (Self.MainComponent == null) return;
        Self.MainComponent.RequireLayoutUpdate = true;
      }
    }

    public class VisualRestructureNotifier_Part : Part
    {
      public void Notify()
      {
        if (Self.MainComponent == null) return;
        Self.MainComponent.RequireVisualRestructure = true;
      }
    }
  }
}