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
  //TODO This class depends on CUICore, it probably should be inside
  public class TextureBuilder2
  {
    private Color[] data;
    private CUIRenderTarget2D target;
    private CUISpriteBatch SpriteBatch;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public PaintingMode PaintingMode { get; set; }

    public TextureBuilder2 ChangeMode(PaintingMode paintingMode)
    {
      PaintingMode = paintingMode;
      return this;
    }

    public CUITexture2D Build()
    {
      target.SetData(data);
      return target;
    }

    public TextureBuilder2 Clear(Color? color = null)
    {
      color ??= Color.Transparent;
      Array.Fill(data, color.Value);

      return this;
    }

    public TextureBuilder2 Start(int width, int height)
    {
      target?.Dispose();

      Width = width;
      Height = height;
      data = new Color[width * height];
      target = CUIRenderTarget2D.Create(width, height);

      return this;
    }

    public TextureBuilder2 Load(string key)
    {
      target?.Dispose();

      CUITexture2D texture = CUICore.TextureManager.Get(key);

      Width = texture.Width;
      Height = texture.Height;
      data = new Color[Width * Height];

      texture.GetData(data);

      target = CUIRenderTarget2D.Create(Width, Height);
      target.SetData(data);

      texture.Dispose();

      return this;
    }

    public TextureBuilder2 SetPixel(int x, int y, Color color)
    {
      int i = x + y * Width;
      Color prev = data[i];

      switch (PaintingMode)
      {
        case PaintingMode.Replace:
          data[i] = color;
          break;
        case PaintingMode.AlphaBlend:
          data[i] = color.Over(prev);
          break;
        case PaintingMode.Mask:
          data[i] = color.Mask(prev);
          break;
        case PaintingMode.InvertMask:
          data[i] = color.InvertMask(prev);
          break;
      }

      return this;
    }

    public TextureBuilder2 Render(
      Action<CUISpriteBatch> renderFunc,
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    )
    {
      CUICore.GraphicsDevice.SetRenderTarget(target); //It actually fills the target with black
      target.SetData(data);

      SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);

      renderFunc(SpriteBatch);

      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);

      target.GetData(data);
      return this;
    }

    public TextureBuilder2 Redraw(
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    )
    {
      CUITexture2D buff = CUITexture2D.Create(Width, Height);
      buff.SetData(data);

      CUICore.GraphicsDevice.SetRenderTarget(target); //It actually fills the target with black

      SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
      SpriteBatch.Draw(buff, target.Bounds, Color.White);
      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);
      buff.Dispose();

      target.GetData(data);
      return this;
    }

    /// <summary>
    /// Have no idea how it works
    /// </summary>
    public TextureBuilder2 Damage(float aCutoff = 0.0f, float cCutoff = 0.2f)
    {
      // DamageEffect.CurrentTechnique = DamageEffect.Techniques["StencilShader"];
      CUICore.GraphicEffects.DamageEffect.Parameters["aCutoff"].SetValue(aCutoff);
      CUICore.GraphicEffects.DamageEffect.Parameters["cCutoff"].SetValue(cCutoff);
      // DamageEffect.CurrentTechnique.Passes[0].Apply();

      Redraw(effect: CUICore.GraphicEffects.DamageEffect);

      return this;
    }

    /// <summary>
    /// Have no idea how to use it, found it in legacy
    /// </summary>
    public TextureBuilder2 Blur(float amount = 0.002f)
    {
      CUICore.GraphicEffects.BlurEffect.SetParameters(amount, amount);
      Redraw(effect: CUICore.GraphicEffects.BlurEffect.Effect);

      CUICore.GraphicEffects.BlurEffect.SetParameters(amount, -amount);
      Redraw(effect: CUICore.GraphicEffects.BlurEffect.Effect);

      return this;
    }

    public TextureBuilder2 Draw(TextureBuilder2 other)
    {
      if (Width != other.Width || Height != other.Height) throw new Exception("Size mismatch");

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          SetPixel(x, y, other.data[x + y * Width]);
        }
      }

      return this;
    }

    public TextureBuilder2 Fill(Func<int, int, Color> fillFunc)
    {
      ArgumentNullException.ThrowIfNull(fillFunc);

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          SetPixel(x, y, fillFunc(x, y));
        }
      }

      return this;
    }

    public TextureBuilder2 Fill(Func<Vector2, Color> fillFunc)
    {
      ArgumentNullException.ThrowIfNull(fillFunc);

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          SetPixel(x, y, fillFunc(new Vector2(x, y)));
        }
      }

      return this;
    }



    public TextureBuilder2 DrawCircle(Vector2 origin, float radius, Color color)
    {
      Rectangle affected = new Rectangle(
        (int)(origin.X - radius),
        (int)(origin.Y - radius),
        (int)radius * 2 + 1,
        (int)radius * 2 + 1
      );

      if (!affected.Intersects(target.Bounds)) return this;

      int minX = Math.Max(0, affected.Left);
      int minY = Math.Max(0, affected.Top);
      int maxX = Math.Min(target.Width, affected.Right);
      int maxY = Math.Min(target.Height, affected.Bottom);

      float r2 = radius * radius;

      for (int y = minY; y < maxY; y++)
      {
        for (int x = minX; x < maxX; x++)
        {
          Vector2 v = new Vector2(x - origin.X, y - origin.Y);

          if (v.LengthSquared() <= r2)
          {
            SetPixel(x, y, color);
          }
        }
      }

      return this;
    }

    public TextureBuilder2 DrawRadialGradient(Vector2 origin, float rFrom, float rTo, Color clFrom, Color clTo)
    {
      if (rTo < rFrom)
      {
        (rFrom, rTo) = (rTo, rFrom);
        (clFrom, clTo) = (clTo, clFrom);
      }

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
            float lambda = (length2 - rFrom2) / rDiff2;
            SetPixel(x, y, Color.Lerp(clFrom, clTo, lambda));
          }
        }
      }

      return this;
    }

    public TextureBuilder2 DrawRing(Vector2 origin, float radius, Color color, float thickness = 0.5f, float fade = 2)
    {
      float ringSize = thickness + fade;

      Rectangle affected = new Rectangle(
        (int)(origin.X - (radius + ringSize)),
        (int)(origin.Y - (radius + ringSize)),
        (int)((radius + ringSize) * 2 + 1),
        (int)((radius + ringSize) * 2 + 1)
      );

      if (!affected.Intersects(target.Bounds)) return this;

      int minX = Math.Max(0, affected.Left);
      int minY = Math.Max(0, affected.Top);
      int maxX = Math.Min(target.Width, affected.Right);
      int maxY = Math.Min(target.Height, affected.Bottom);



      for (int y = minY; y < maxY; y++)
      {
        for (int x = minX; x < maxX; x++)
        {
          float r = new Vector2(x - origin.X, y - origin.Y).Length();

          float rDiff = Math.Abs(r - radius);

          if (rDiff > ringSize) continue;

          if (rDiff < thickness)
          {
            SetPixel(x, y, color);
            continue;
          }


          float lambda = (rDiff - thickness) / fade;
          SetPixel(x, y, Color.Lerp(color, Color.Transparent, lambda));
        }
      }

      return this;
    }

    public TextureBuilder2 DrawRingSector(RingSegmentParams args)
    {
      Vector2 realOrigin = args.Origin +
        new Vector2(
          (float)Math.Cos(args.MidAngle),
          (float)Math.Sin(args.MidAngle)
        ) * args.Offset;


      Rectangle affected = new Rectangle(
        (int)(realOrigin.X - args.OuterFadeRadius),
        (int)(realOrigin.Y - args.OuterFadeRadius),
        (int)(args.OuterFadeRadius * 2 + 1),
        (int)(args.OuterFadeRadius * 2 + 1)
      );

      if (!affected.Intersects(target.Bounds)) return this;

      int minX = Math.Max(0, affected.Left);
      int minY = Math.Max(0, affected.Top);
      int maxX = Math.Min(target.Width, affected.Right);
      int maxY = Math.Min(target.Height, affected.Bottom);


      for (int y = minY; y < maxY; y++)
      {
        for (int x = minX; x < maxX; x++)
        {
          Vector2 v = new Vector2(x - realOrigin.X, y - realOrigin.Y);
          float r = v.Length();
        }
      }

      return this;
    }

    public TextureBuilder2()
    {
      SpriteBatch = CUISpriteBatch.Create();
    }

    public TextureBuilder2(int width, int height) : this()
    {
      Start(width, height);
    }

  }

}