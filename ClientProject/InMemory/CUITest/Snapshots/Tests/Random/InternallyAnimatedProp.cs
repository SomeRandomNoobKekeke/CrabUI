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
      public static CUIComponent InternallyAnimatedProp()
      {
        CUIComponent frame = new CUIDefault.Frame("InternallyAnimatedProp", 400, 600);

        frame.Background.Animations.Color.Duration = 3;

        frame["color input"] = new CUITextInput()
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(w: 100, h: 24),
          OnInput = (text) => frame.Background.Color = CUICore.Parser.Parse<Color>(text),
        };

        return frame;
      }
    }
  }
}