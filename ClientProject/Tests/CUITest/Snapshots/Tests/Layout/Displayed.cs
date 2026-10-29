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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Layout
    {
      public static CUIComponent Displayed()
      {
        CUIFrame frame = new CUIDefault.Frame("Displayed", 400, 600);

        frame["layout"]["toggle"] = new CUIToggleButton("Displayed")
        {
          State = true,
          OnToggle = (state) => frame["layout"]["2"].Displayed = state,
        };

        frame["layout"]["1"] = new CUITextBlock("text block 1");
        frame["layout"]["2"] = new CUITextBlock("text block 2");
        frame["layout"]["3"] = new CUITextBlock("text block 3");

        return frame;
      }
    }
  }
}