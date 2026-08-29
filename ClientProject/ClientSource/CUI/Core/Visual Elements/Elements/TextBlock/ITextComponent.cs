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
  /// <summary>
  /// Common interface for CUIComponents with text 
  /// Just so i could test text props on them all at once
  /// </summary>
  public interface ITextComponent
  {
    public string Text { get; set; }
    public float Scale { get; set; }
    public ResizeStrategy ResizeStrategy { get; set; }
    public Vector2 TextAnchor { get; set; }
    public Color TextColor { get; set; }
    public SpriteEffects SpriteEffects { get; set; }
    public float LayerDepth { get; set; }
    public CUIFont Font { get; set; }

    public string RealText { get; }
    public Vector2 RawTextSize { get; }
    public Vector2 TextDrawPosition { get; }
    public CUINullVector2 ForcedSize { get; }
    public float RealScale { get; }

  }
}