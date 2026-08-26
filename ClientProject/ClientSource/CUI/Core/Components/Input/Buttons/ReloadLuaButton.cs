using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  /// <summary>
  /// Used surprisingly often 
  /// </summary>
  public class ReloadLuaButton : CUIButton
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<ReloadLuaButton>((c) =>
    {
      c.MasterColor = Color.Lerp(c.Palette["main"], Color.Red, 0.5f);
    });

    public ReloadLuaButton() : base("Reload Lua")
    {
      MouseDown += (e) => DebugConsole.ExecuteCommand("cl_reloadlua");
    }
  }

}