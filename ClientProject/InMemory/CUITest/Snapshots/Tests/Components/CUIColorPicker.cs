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
    public static partial class Components
    {
      public static CUIComponent CUIColorPicker()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIColorPicker", 400, 600);

        frame["wrapper"] = new CUIVerticalList()
        {
          FitContent = new CUIBool2(true, true),
          Anchor = CUIAnchor.Center,
        };

        frame["wrapper"]["CUIColorSelect"] = new CUIColorPicker()
        {
          Absolute = new CUINullRect(w: 210, h: 180),
          HueSelectWidth = 30,
          OnSelected = (cl) =>
          {
            frame.Get<CUITextBlock>("wrapper.label").Text = CUICore.Parser.Serialize(cl);
            frame.Get<CUITextBlock>("wrapper.label").Background.Color = cl;
          },

        };

        frame["wrapper"]["label"] = new CUITextBlock("Color");

        return frame;
      }
    }
  }
}