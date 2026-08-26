using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
namespace CursedUI
{
  public partial record CUISprite
  {
    //TODO how to UpdateDataBuffer if user changes data in texture manually? 
    private Color[] DataBuffer;
    public void UpdateDataBuffer()
    {
      DataBuffer = ShouldBufferData ? Texture.Data : [];
    }

    public bool _ShouldBufferData; public bool ShouldBufferData
    {
      get => _ShouldBufferData;
      set
      {
        if (ShouldBufferData == value) return;
        _ShouldBufferData = value;
        UpdateDataBuffer();
      }
    }

    public Color[] Data => ShouldBufferData ? DataBuffer : Texture.Data;

    /// <param name="point">([0..1], [0..1])</param>
    public Color GetPixel(Vector2 point)
    {
      Rectangle SourceRect = SourceRectangle.HasValue ? SourceRectangle.Value : Texture.Bounds;

      int textureX = (int)Math.Floor(SourceRect.X + point.X * SourceRect.Width);
      int textureY = (int)Math.Floor(SourceRect.Y + point.Y * SourceRect.Height);

      if (textureX < SourceRect.X || (SourceRect.X + SourceRect.Width - 1) < textureX) return Color.Transparent;
      if (textureY < SourceRect.Y || (SourceRect.Y + SourceRect.Height - 1) < textureY) return Color.Transparent;

      return Data[textureY * Texture.Width + textureX];
    }

    public bool IsPointOnTransparentPixel(Vector2 point) => GetPixel(point).A == 0;
  }
}