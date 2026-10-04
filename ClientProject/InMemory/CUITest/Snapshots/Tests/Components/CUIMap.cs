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
      public static CUIComponent CUIMap()
      {
        var frame = new CUIDefault.Frame("CUIMap", 400, 600);

        CUIMap map = null;



        frame["layout"]["zoom"] = new CUIRangeInput()
        {
          Absolute = new CUINullRect(h: 30),
          OnHandleDragged = (v) =>
          {
            map.Zoom = v;
          },
        };


        frame["layout"]["map"] = map = new CUIMap()
        {
          Flex = 1,
          Background =
          {
            Sprite = CUISprite.Vignette,
            Color = Color.DarkBlue,
          }
        };

        map["frame1"] = new CUIDefault.Frame("frame 1")
        {
          Anchor = CUIAnchor.LeftTop,
          Absolute = new CUINullRect(0, 100, 200, 300),
          Palette = CUICore.Palettes.Secondary
        };
        map["frame1"]["layout"]["text1"] = new CUITextBlock("text1");
        map["frame1"]["layout"]["text2"] = new CUITextBlock("text2");
        map["frame1"]["layout"]["text3"] = new CUITextBlock("text3");

        map["frame2"] = new CUIDefault.Frame("frame 2")
        {
          Palette = CUICore.Palettes.Tertiary,
          Anchor = CUIAnchor.LeftTop,
          Absolute = new CUINullRect(500, 100, 200, 300),
        };
        map["frame2"]["layout"]["text1"] = new CUITextBlock("text1");
        map["frame2"]["layout"]["text2"] = new CUITextBlock("text2");
        map["frame2"]["layout"]["text3"] = new CUITextBlock("text3");

        return frame;
      }
    }
  }
}