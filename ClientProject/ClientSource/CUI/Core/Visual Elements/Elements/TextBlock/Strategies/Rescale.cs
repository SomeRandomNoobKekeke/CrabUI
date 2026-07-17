using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;

namespace CrabUI
{
  public partial class TextBlock
  {
    public class RescaleStrategy : ResizeStrategyBase
    {
      public override void MeasureRawTextSize(string text, float scale, CUIFont font)
      {
        RawTextSize = font.MeasureString(text);
        RealText = text;
      }

      public override void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale, CUIFont font)
      {
        RealScale = scale;

        Vector2 RealTextSize = RawTextSize * RealScale;


        if (RealTextSize.X > rect.Width || RealTextSize.Y > rect.Height)
        {
          RealScale = Math.Min(RealScale, rect.Width / RealTextSize.X);
          RealScale = Math.Min(RealScale, rect.Height / RealTextSize.Y);
          RealTextSize = RawTextSize * RealScale;
        }

        RealText = text;
        TextDrawPosition = CUIAnchor.ChildPosIn(rect, anchor, RealTextSize);
      }
    }
  }

}