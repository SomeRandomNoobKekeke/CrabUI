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
    public class ResistStrategy : ResizeStrategyBase
    {
      public override void MeasureRawTextSize(string text, float scale, CUIFont font)
      {
        RawTextSize = font.MeasureString(text) * scale;
        ForcedSize = new CUINullVector2(RawTextSize);
        RealText = text;
        RealScale = scale;
      }

      public override void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale, CUIFont font)
      {
        TextDrawPosition = CUIAnchor.ChildPosIn(rect, anchor, RawTextSize);
      }
    }
  }

}