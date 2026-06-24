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
  public partial class CUIDebugger
  {
    public CUIButton OpenButton;
    public MainFrameComponent MainFrame;
    public bool IsOpen
    {
      get => MainFrame.IsOpen;
      set => MainFrame.IsOpen = value;
    }

    public void Init()
    {
      CreateUI();
      CUI.TopMain["open debug"] = OpenButton;
    }

    public void CreateUI()
    {
      OpenButton = new CUIButton("Debug")
      {
        Anchor = CUIAnchor.LeftCenter,
        AddMouseDown = (c, e) => IsOpen = true,
      };

      MainFrame = new MainFrameComponent();
    }

    public void Dispose()
    {
      OpenButton.RemoveSelf();
      MainFrame.Close();
    }
  }
}