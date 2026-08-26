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
      public static CUIComponent Default()
      {
        CUIFrame frame = new CUIDefault.Frame("bruh")
        {
          Absolute = new CUINullRect(w: 400, h: 600),
        };

        CUITextInput input = new CUITextInput()
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(w: 100, h: 24),
        };

        input.Input += (s) => CUICore.Palettes.Primary = CUIPalette.FromColor(CUICore.Parser.Parse<Color>(s));

        frame["theme"] = input;

        return frame;
      }
    }
  }
}