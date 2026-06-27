using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public class MGDebugTool
  {
    public CUIFrame Frame { get; set; }
    public CUIButton ToggleButton { get; set; }
    public CUIMagnifyingGlass MG { get; set; }

    public void Init()
    {
      CreateUI();
      CUI.TopMain["mgbutton"] = ToggleButton;
    }

    public void Dispose()
    {
      ToggleButton.RemoveSelf();
      Frame.Close();
      MG.Dispose();
    }

    public void CreateUI()
    {
      Frame = new CUIFrame()
      {
        Absolute = new CUINullRect(w: 200, h: 200),
        Anchor = CUIAnchor.Center,
        TargetMainComponent = CUI.TopMain,
        Background = { Color = Color.Cyan },
        ConsumeMouseClicks = true,
      };

      Frame["mg"] = MG = new CUIMagnifyingGlass()
      {
        Background = { Color = Color.White },
        Relative = new CUINullRect(0.05f, 0.05f, 0.9f, 0.9f),
        Size = new Point(40, 40),
      };

      ToggleButton = new CUIButton()
      {
        Text = "MG",
        Absolute = new CUINullRect(w: 30, h: 20),
        Anchor = new Vector2(0, 0.54f),
        OnMouseDown = (c, e) =>
        {
          Frame.IsOpen = !Frame.IsOpen;
          MG.Active = Frame.IsOpen;
        },
      };
    }
  }
}