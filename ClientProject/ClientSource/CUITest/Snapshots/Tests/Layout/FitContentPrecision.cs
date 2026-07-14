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

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Layout
    {
      public static CUIComponent FitContentPrecision()
      {
        CUIFrame frame = new CUIDefault.Frame("FitContentPrecision")
        {
          Absolute = new CUINullRect(0, 0, 400, 600),
        };

        frame["layout"]["list"] = new CUIVerticalList()
        {
          FitContent = new CUIBool2(true, true),
          Background = { Color = Color.Yellow },
        };

        frame["layout"]["list"]["1"] = new CUIComponent()
        {
          Absolute = new CUINullRect(w: 50.0f, h: 20.3f),
          Background = { Color = Color.Green * 0.5f },
        };

        frame["layout"]["list"]["2"] = new CUIComponent()
        {
          Absolute = new CUINullRect(w: 100.0f, h: 20.3f),
          Background = { Color = Color.Red * 0.5f },
        };

        frame["layout"]["list"]["3"] = new CUIComponent()
        {
          Absolute = new CUINullRect(w: 50.0f, h: 20.3f),
          Background = { Color = Color.Blue * 0.5f },
        };

        return frame;
      }
    }
  }
}