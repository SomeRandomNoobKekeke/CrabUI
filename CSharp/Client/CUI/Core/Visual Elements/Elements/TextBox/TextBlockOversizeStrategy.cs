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

  public abstract class TextBlockOversizeStrategy
  {
    public abstract void MeasureRawTextSize(string text, float scale, CUIFont font);
    public abstract void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale);

    public Vector2 RawTextSize { get; private set; }
    public Vector2 TextDrawPosition { get; private set; }
    public CUINullVector2 ForcedSize { get; private set; }
  }
}