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
    public static partial class Components
    {
      public static CUIComponent VerticalTextBlocks()
      {
        CUIFrame frame = new CUIDefault.Frame("VerticalTextBlocks", 400, 600);

        frame["layout"].Background.Color = Color.Wheat;

        frame["layout"]["text 1"] = new CUITextBlock("bebebe")
        {
          Vertical = true,
          Background = { Color = Color.Blue },
        };

        return frame;
      }
    }
  }
}