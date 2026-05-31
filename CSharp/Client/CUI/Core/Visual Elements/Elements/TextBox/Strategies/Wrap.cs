using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaroJunk;

namespace CrabUI
{
  public partial class TextBlock
  {
    public class WrapStrategy : ResizeStrategyBase
    {
      public override void MeasureRawTextSize(string text, float scale, CUIFont font)
      {
        RawTextSize = font.MeasureString(text);
      }

      public override void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale, CUIFont font)
      {
        Vector2 RealTextSize = RawTextSize * scale;
        RealText = text;
        RealScale = scale;


        if (RealTextSize.X > rect.Width)
        {
          RealText = font.WrapText(text, rect.Width);
          RealTextSize = font.MeasureString(RealText) * scale;
        }

        TextDrawPosition = CUIAnchor.ChildPosIn(rect, anchor, RealTextSize);
      }
    }
  }

}