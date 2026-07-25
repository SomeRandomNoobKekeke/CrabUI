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
  public partial class CUIComponent
  {
    public override void OnDebugOn()
    {
      DebugRelays.Open();
      base.OnDebugOn();
    }

    public override void OnDebugOff()
    {
      DebugRelays.Close();
      base.OnDebugOff();
    }
  }
}