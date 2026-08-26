using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Layout
    {
      public static CUIComponent FitContentPlain()
      {

        CUIFrame frame = new CUIDefault.Frame("FitContentPlain", 400, 600);

        CUIComponent CreateWrapper(Vector2 pos) => new CUIComponent()
        {
          Absolute = new CUINullRect(x: pos.X, y: pos.Y),
          FitContent = new CUIBool2(true, true),
          Background = { Color = Color.Yellow },
        };

        CUIComponent CreateAnchor(string text) => new CUITextBlock()
        {
          Text = text,
          Absolute = new CUINullRect(50, 50),
          Background = { Color = Color.Orange },
          Padding = new CUISizes(5, 5, 5, 5),

          Margin = new CUISizes(5, 5, 5, 5),
          Borders =
          {
            Sizes = new CUISizes(5, 5, 5, 5),
            Color = Color.Cyan,
          }
        };


        frame["wrapper1"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 150, 30, 30),
          FitContent = new CUIBool2(true, true),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper1"]["text"] = CreateAnchor("fit x,y");


        frame["wrapper2"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 300, 200, 30),
          FitContent = new CUIBool2(false, true),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper2"]["text"] = CreateAnchor("fit y");


        frame["wrapper3"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 450, 200, 60),
          FitContent = new CUIBool2(true, false),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper3"]["text"] = CreateAnchor("fit x");

        return frame;
      }
    }
  }
}