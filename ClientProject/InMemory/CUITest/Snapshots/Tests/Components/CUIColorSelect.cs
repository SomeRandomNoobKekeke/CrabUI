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
    public static partial class Components
    {
      public static CUIComponent CUIColorSelect()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIColorSelect", 400, 600);

        frame["wrapper"] = new CUIVerticalList()
        {
          FitContent = new CUIBool2(true, true),
          Anchor = CUIAnchor.Center,
        };

        frame["wrapper"]["CUIColorSelect"] = new CUIColorSelect()
        {

          Absolute = new CUINullRect(w: 256, h: 256),
          Background = {
            Sprite = CUISprite.CreateRadialColorPicker(256, 256),
          },
          OnSelected = (cl) =>
          {
            frame.Get<CUITextBlock>("wrapper.label").Text = CUICore.Parser.Serialize(cl);
            frame.Get<CUITextBlock>("wrapper.label").Background.Color = cl;
          }
        };

        frame["wrapper"]["label"] = new CUITextBlock("Color");

        return frame;
      }
    }
  }
}