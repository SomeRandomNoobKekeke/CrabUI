using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      /// <summary>
      /// Idk, for some reason in similar setup "main" buttons sometimes steal clicks from "header" button even though they are clearly below
      /// </summary>
      /// <returns></returns>
      public static CUIComponent WeirdGhostClicks()
      {
        CUIFrame frame = new CUIDefault.Frame("WeirdGhostClicks", 400, 600);

        frame["layout"]["header"] = new CUIDefault.HorizontalPanel()
        {
          FitContent = new CUIBool2(false, true),
          Scrollable = true,
          Palette = CUICore.Palettes.Secondary,
        };

        for (int i = 0; i < 10; i++)
        {
          frame["layout"]["header"].Children.Add(new CUIButton("bruh")
          {
            OnMouseDown = (e) => CUI.Logger.Log("header"),
            InheritPalette = true,
          });
        }

        frame["layout"]["header2"] = new CUIDefault.HorizontalPanel()
        {
          FitContent = new CUIBool2(false, true),
          Scrollable = true,
          Palette = CUICore.Palettes.Secondary,
          ConsumeMouseEvents = true,
        };

        for (int i = 0; i < 10; i++)
        {
          frame["layout"]["header2"].Children.Add(new CUIButton("bruh")
          {
            OnMouseDown = (e) => CUI.Logger.Log("header"),
            InheritPalette = true,
          });
        }

        frame["layout"]["main"] = new CUIVerticalList()
        {
          Flex = 1,
          Scrollable = true,
        };

        for (int i = 0; i < 100; i++)
        {
          frame["layout"]["main"].Children.Add(new CUIButton($"{i}")
          {
            OnMouseDown = (e) => CUI.Logger.Log("main")
          });
        }

        return frame;
      }
    }
  }
}