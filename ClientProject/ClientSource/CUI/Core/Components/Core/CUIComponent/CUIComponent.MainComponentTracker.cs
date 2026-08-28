using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIComponent
  {
    protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      DebugRelays.Map(MainComponent.DebugRelays);
    }
    protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      DebugRelays.Unmap(MainComponent.DebugRelays);
      RightResizeHandle.ForceRelease();
    }

  }


}