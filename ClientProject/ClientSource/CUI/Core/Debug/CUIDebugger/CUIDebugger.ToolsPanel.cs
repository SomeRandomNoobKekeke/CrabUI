using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;

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
      wrapper["Hint"] = new CUITextBlock("Tools: ") { };


      wrapper["mg"] = new CUIButton("Magnifying Glass")
      {
        OnMouseDown = (e) =>
        {
          MGFrame.Absolute = MGFrame.Absolute with { Position = new Vector2(0, 0) };
          MGFrame.Toggle();
        },
      };

      // list.DeepPalette = CUICore.Palettes.Tertiary;

      return wrapper;
    }
  }
}