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
namespace CursedUI
{

  //CRINGE but at least it's fast and not scattered all over the place
  public partial class CUIVisualComponent
  {
    protected void NotifyLayoutChanged()
    {
      if (MainComponent == null) return;
      MainComponent.RequireLayoutUpdate = true;
    }

    protected void NotifyTreeChanged()
    {
      Tree.PropogateTreeChanged();
    }

    protected void NotifyVisualsRestructured()
    {
      if (MainComponent == null) return;
      MainComponent.RequireVisualRestructure = true;
    }
  }
}