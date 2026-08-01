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
  public partial class TextureBuilder
  {
    private Color[] data;
    // private CUIRenderTarget2D target;
    private CUISpriteBatch SpriteBatch;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public PaintingMode PaintingMode { get; set; }

    public TextureBuilder ChangeMode(PaintingMode paintingMode)
    {
      PaintingMode = paintingMode;
      return this;
    }

    public CUITexture2D Build(bool tracked = false)
    {

      CUIRenderTarget2D target = tracked ?
        CUICore.TextureManager.CreateNewRenderTarget(Width, Height) :
        CUIRenderTarget2D.Create(Width, Height);

      target.SetData(data);
      return target;
    }

    public TextureBuilder Start(int width, int height)
    {
      Width = width;
      Height = height;
      data = new Color[width * height];

      return this;
    }

    public TextureBuilder Clear(Color? color = null)
    {
      color ??= Color.Transparent;
      Array.Fill(data, color.Value);

      return this;
    }

    public TextureBuilder Load(string key)
    {
      CUITexture2D texture = CUICore.TextureManager.Get(key);

      Width = texture.Width;
      Height = texture.Height;
      data = new Color[Width * Height];

      texture.GetData(data);

      return this;
    }

    public TextureBuilder SetPixel(int x, int y, Color color)
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

    public TextureBuilder Edit(Action<Color[]> action)
    {
      action(data);
      return this;
    }


    public TextureBuilder Render(
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
      CUIRenderTarget2D target = CUIRenderTarget2D.Create(Width, Height);

      CUICore.GraphicsDevice.SetRenderTarget(target); //It actually fills the target with black
      target.SetData(data);

      SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);

      renderFunc(SpriteBatch);

      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);

      target.GetData(data);
      target.ForgetAndDispose();

      return this;
    }

    public TextureBuilder Redraw(
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    )
    {
      CUIRenderTarget2D target = CUIRenderTarget2D.Create(Width, Height);
      target.SetData(data);

      CUICore.GraphicsDevice.SetRenderTarget(target); //It actually fills the target with black

      SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
      SpriteBatch.Draw(target, target.Bounds, Color.White);
      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);
      target.GetData(data);
      target.ForgetAndDispose();

      return this;
    }

    public TextureBuilder Draw(TextureBuilder other)
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

    public TextureBuilder Fill(Func<int, int, Color> fillFunc)
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

    public TextureBuilder Fill(Func<Vector2, Color> fillFunc)
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



    public TextureBuilder()
    {
      SpriteBatch = CUISpriteBatch.Create();
    }

    public TextureBuilder(int width, int height) : this()
    {
      Start(width, height);
    }


  }

}