using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace CursedUI
{
  public class FastTextDump : VisualElementBase
  {
    public record struct Line(string Text, Color Color);


    private CUIRect _Rect; public CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
      }
    }

    public override bool Contains(Vector2 pos) => Rect.Contains(pos);


    public List<Line> Lines { get; set; } = [];

    public void AddLine(string text, Color? color = null)
    {
      Lines.Add(new Line(text, color ?? Color.White));
    }

    public void Clear() => Lines.Clear();

    public bool Solid { get; set; }
    public CUINullVector2 ForcedSize => Solid ?
      new CUINullVector2(null, Offset.Y + Lines.Count * Shift.Y) :
      new CUINullVector2(null, null);



    //TODO what about funny curved draw patterns?
    public Vector2 Shift { get; set; } = new Vector2(0.0f, 22.0f);
    public Vector2 Offset { get; set; }
    public float Rotation { get; set; } = 0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1.0f;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.1f;
    public Alignment TextAlignment { get; set; } = Alignment.TopLeft;
    public ForceUpperCase ForceUpperCase { get; set; } = ForceUpperCase.Inherit;

    public CUIFont Font { get; set; } = CUIFont.Font;


    public override void Draw(CUISpriteBatch spriteBatch)
    {
      Vector2 pos = Rect.LeftTop + Offset;
      if (Visible)
      {
        foreach (Line line in Lines)
        {
          Font.DrawString(
            spriteBatch,
            line.Text,
            pos,
            line.Color,
            Rotation,
            Origin,
            Scale,
            SpriteEffects,
            LayerDepth,
            TextAlignment,
            ForceUpperCase
          );
          pos += Shift;
        }
      }
    }
  }
}