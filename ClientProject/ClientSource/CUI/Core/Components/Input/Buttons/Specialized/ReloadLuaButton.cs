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
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<ReloadLuaButton>((c) =>
    {
      c.MasterColor = new Color(255, 127, 0);
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Background.Sprite = CUISprite.VerticalGlow128;
    }

    public ReloadLuaButton() : base("Reload Lua")
    {
      MouseDown += (e) => DebugConsole.ExecuteCommand("cl_reloadlua");
    }
  }

}