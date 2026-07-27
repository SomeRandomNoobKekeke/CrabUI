using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class TextureBuilder
  {
    private Color[] data;
    private CUIRenderTarget2D target;
    private CUISpriteBatch SpriteBatch;
    public int Width { get; }
    public int Height { get; }

    public CUITexture2D Build()
    {
      target.SetData(data);
      return target;
    }

    public TextureBuilder Clear(Color? color = null)
    {
      color ??= Color.Transparent;
      Array.Fill(data, color.Value);

      return this;
    }

    public TextureBuilder Render(Action<CUISpriteBatch> renderFunc)
    {
      CUICore.GraphicsDevice.SetRenderTarget(target); //It actually fills the target with black
      target.SetData(data);

      // //TODO save and restore scissor rect
      SpriteBatch.Begin(samplerState: CUICore.SamplerState, rasterizerState: CUICore.RasterizerState);

      renderFunc(SpriteBatch);

      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);

      target.GetData(data);
      return this;
    }

    public TextureBuilder Fill(Func<int, int, Color> fillFunc)
    {
      ArgumentNullException.ThrowIfNull(fillFunc);

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          data[x + y * Width] = fillFunc(x, y);
        }
      }

      return this;
    }

    public TextureBuilder Fill(Func<Vector2, Color> fillFunc)
    {
      ArgumentNullException.ThrowIfNull(fillFunc);

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          data[x + y * Width] = fillFunc(new Vector2(x, y));
        }
      }

      return this;
    }

    public TextureBuilder DrawCircle(Vector2 origin, float radius, Color color)
    {
      float r2 = radius * radius;

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          Vector2 diff = new Vector2(x - origin.X, y - origin.Y);

          if (diff.LengthSquared() <= r2) data[x + y * Width] = color;
        }
      }

      return this;
    }

    public TextureBuilder DrawRadialGradient(Vector2 origin, float rFrom, float rTo, Color clFrom, Color clTo)
    {
      float rFrom2 = rFrom * rFrom;
      float rTo2 = rTo * rTo;

      float rDiff2 = rTo2 - rFrom2;

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          Vector2 diff = new Vector2(x - origin.X, y - origin.Y);
          float length2 = diff.LengthSquared();

          if (rFrom2 <= length2 && length2 <= rTo2)
          {
            int i = x + y * Width;
            // Color existingColor = data[i]; ///TODO idk how to blend colors

            float lambda = (length2 - rFrom2) / rDiff2;

            data[i] = Color.Lerp(clFrom, clTo, lambda);
          }
        }
      }


      return this;
    }


    public TextureBuilder(int width, int height)
    {
      Width = width;
      Height = height;
      data = new Color[width * height];
      target = CUIRenderTarget2D.Create(width, height);
      SpriteBatch = CUISpriteBatch.Create();
    }

  }

}