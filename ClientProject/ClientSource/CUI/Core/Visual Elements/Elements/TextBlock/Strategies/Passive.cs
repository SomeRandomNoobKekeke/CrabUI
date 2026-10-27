using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;

namespace CursedUI
{
  public partial class TextBlock
  {
    public class PassiveStrategy : ResizeStrategyBase
    {
      public override void MeasureRawTextSize(string text, float scale, CUIFont font, bool vertical)
      {
        if (vertical) text = font.WrapText(text, 0).Trim('\n');

        RawTextSize = font.MeasureString(text);
        RealText = text;
      }

      public override void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale, CUIFont font, bool vertical)
      {
        Vector2 RealTextSize = RawTextSize * scale;
        RealScale = scale;

        RealText = text;
        TextDrawPosition = CUIAnchor.ChildPosIn(rect, anchor, RealTextSize);
      }
    }
  }

}