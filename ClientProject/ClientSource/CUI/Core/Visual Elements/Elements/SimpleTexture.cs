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

namespace CrabUI
{
  public class SimpleTexture : VisualElementBase, CUISerializable, IFocusRequestEventConsumer
  {
    public DebugNode<CUIRect, Rectangle> Debug_RoundedRect { get; } = new(
      DebugCategory.RoundedRect, CUI.DebugHub,
      (rect, rounded) => $"{rect} -> {rounded}"
    );

    public bool ConsumeFocus { get; set; }
    public ClearableEvent<CUIFocusRequestEvent> FocusProbed { get; } = new();

    private Rectangle RoundedRect;

    private CUIRect _Rect; public override CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        RoundedRect = _Rect.Round();
        Debug_RoundedRect.Send(_Rect, RoundedRect);
      }
    }

    private CUISprite _Sprite = CUISprite.White; public CUISprite Sprite
    {
      get => _Sprite;
      set
      {
        _Sprite = value;
        _Sprite.ShouldBufferData = IgnoretransparentPixels;
      }
    }

    /// <summary>
    /// Source of truth elevated from sprite
    /// </summary>
    private bool _IgnoretransparentPixels; public bool IgnoretransparentPixels
    {
      get => _IgnoretransparentPixels;
      set
      {
        _IgnoretransparentPixels = value;
        _Sprite.ShouldBufferData = IgnoretransparentPixels;
      }
    }

    public override bool Contains(Vector2 pos)
    {
      if (IgnoretransparentPixels)
      {
        return !Sprite.IsPointOnTransparentPixel(
          (pos - Rect.Position) / Rect.Size
        );
      }
      return Rect.Contains(pos);
    }


    #region Forwarded to CUISprite
    public CUITexture2D Texture { get => Sprite.Texture; set => Sprite.Texture = value; }
    public Rectangle? SourceRectangle { get => Sprite.SourceRectangle; set => Sprite.SourceRectangle = value; }
    [CUISerializableProp]
    public Color Color { get => Sprite.Color; set => Sprite.Color = value; }
    public float Rotation { get => Sprite.Rotation; set => Sprite.Rotation = value; }
    public Vector2 Origin { get => Sprite.Origin; set => Sprite.Origin = value; }
    public SpriteEffects Effects { get => Sprite.Effects; set => Sprite.Effects = value; }
    public float LayerDepth { get => Sprite.LayerDepth; set => Sprite.LayerDepth = value; }

    #endregion

    public override void Draw(CUISpriteBatch spriteBatch)
    {
      if (Visible)
      {
        Sprite.Draw(spriteBatch, RoundedRect);
      }
    }

    public static object Deserialize(XElement element)
    {
      return new SimpleTexture();
    }
    public XElement Serialize() => new XElement(GetType().Name);


  }
}