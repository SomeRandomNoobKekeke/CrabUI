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
    //TODO
    public class WrapStrategy : ResizeStrategyBase
    {
      /// <summary>
      /// arbitrary value
      /// </summary>
      public static float MinWidth = 12;

      public override void MeasureRawTextSize(string text, float scale, CUIFont font)
      {
        RawTextSize = font.MeasureString(text);
        ForcedSize = new CUINullVector2(null, RawTextSize.Y);
        RealText = text;
        RealScale = scale;
      }

      public override void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale, CUIFont font)
      {
        Vector2 RealTextSize = RawTextSize * scale;
        RealText = text;
        ForcedSize = new CUINullVector2(null, RawTextSize.Y);

        if (RealTextSize.X > rect.Width)
        {
          RealText = font.WrapText(text, rect.Width);
          RealTextSize = font.MeasureString(RealText) * scale;
          ForcedSize = new CUINullVector2(null, RealTextSize.Y);
        }

        TextDrawPosition = CUIAnchor.ChildPosIn(rect, anchor, RealTextSize);
      }
    }
  }

}