using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Barotrauma;

namespace CrabUI
{
  public partial class CUIDebugger : CUIDefault.Frame
  {
    private CUIComponent CreateToolsPanel()
    {
      CUIHorizontalList wrapper = new()
      {
        FitContent = new CUIBool2(false, true),
        // Borders = { Bottom = 2 }
      };

      wrapper["Reload Lua"] = new CUIButton("Reload Lua")
      {
        OnMouseDown = (e) => DebugConsole.ExecuteCommand("cl_reloadlua"),
        Style = (c) => c.MasterColor = Color.Lerp(c.Palette["main"], Color.Red, 0.5f),
      };

      wrapper["palettes"] = new CUIButton("Palettes")
      {
        OnMouseDown = (e) => DebugConsole.ExecuteCommand("cuipalettepreview"),
      };

      wrapper["mg"] = new CUIButton("Magnifying Glass")
      {
        OnMouseDown = (e) =>
        {
          MGFrame.Absolute = MGFrame.Absolute with { Position = new Vector2(0, 0) };
          MGFrame.Toggle();
        },
      };

      wrapper.DeepPalette = CUICore.Palettes.Primary;

      return wrapper;
    }
  }
}