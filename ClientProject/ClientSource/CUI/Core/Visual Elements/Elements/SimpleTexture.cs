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
using System.Text.Json;

namespace CursedUI
{
  public class SimpleTexture : VisualElementBase, NestedCUISerializable
  {
    private Rectangle RoundedRect;

    private CUIRect _Rect; public CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        RoundedRect = _Rect.Round();
      }
    }

    /// <summary>
    /// HACK
    /// Expands area where texture captures clicks
    /// Primarily for resize handles
    /// </summary>
    public CUISizes SensorExpansion { get; set; }

    private CUISprite _Sprite = CUISprite.White;

    [CUISerializableProp]
    public CUISprite Sprite
    {
      get => _Sprite;
      set
      {
        _Sprite = value;
        _Sprite.ShouldBufferData = IgnoretransparentPixels;

        if (_Color.Elevated && _Sprite.ColorIsDefault)
        {
          _Sprite.Color = _Color.Value;
        }
      }
    }

    /// <summary>
    /// Source of truth elevated from sprite
    /// </summary>
    private bool _IgnoretransparentPixels;
    [CUISerializableProp]
    public bool IgnoretransparentPixels
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
      return Rect.Contains(pos, SensorExpansion);
    }

    private ElevatedProp<Color> _Color;
    public Color Color //CRINGE so if you change this Color next sprite will inherit it
    {
      get => Sprite.Color;
      set
      {
        Sprite.Color = value;
        _Color.Value = value;
        _Color.Elevated = true;
      }
    }


    #region Forwarded to CUISprite
    public CUITexture2D Texture { get => Sprite.Texture; set => Sprite.Texture = value; }
    public Rectangle? SourceRectangle { get => Sprite.SourceRectangle; set => Sprite.SourceRectangle = value; }
    public float Rotation { get => Sprite.Rotation; set => Sprite.Rotation = value; }
    public Vector2 Origin { get => Sprite.Origin; set => Sprite.Origin = value; }
    public SpriteEffects Effects { get => Sprite.Effects; set => Sprite.Effects = value; }
    public float LayerDepth { get => Sprite.LayerDepth; set => Sprite.LayerDepth = value; }
    public CUISpriteDrawMode DrawMode { get => Sprite.DrawMode; set => Sprite.DrawMode = value; }
    #endregion

    public override void Draw(CUISpriteBatch spriteBatch)
    {
      if (Visible)
      {
        Sprite.Draw(spriteBatch, RoundedRect);
      }
    }
  }
}