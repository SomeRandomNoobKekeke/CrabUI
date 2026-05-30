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
        ForcedSize = new CUINullVector2(RawTextSize);
      }

      public override void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale)
      {
        Vector2 RealTextSize = RawTextSize * scale;

        TextDrawPosition = CUIAnchor.ChildPosIn(rect, anchor, RealTextSize);
      }
    }
  }

}