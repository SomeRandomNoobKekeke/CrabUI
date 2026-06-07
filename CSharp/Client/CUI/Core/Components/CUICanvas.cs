using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  /// <summary>
  /// Allows you to manipulate pixel data of its texture
  /// </summary>
  public class CUICanvas : CUIComponent, IDisposable
  {
    public Color[] Data;
    public CUIRenderTarget2D Texture;
    public CUISpriteBatch SpriteBatch;


    public virtual Point Size
    {
      get => new Point(Texture.Width, Texture.Height);
      set
      {
        if (value.X == Texture?.Width && value.Y == Texture?.Height) return;

        CUIRenderTarget2D oldTexture = Texture;
        Texture = CUIRenderTarget2D.Create(value.X, value.Y);
        Data = new Color[Texture.Width * Texture.Height];

        //TODO use sprite
        Background.Texture = Texture;

        oldTexture?.Dispose();
      }
    }

    public void Clear(Color? color = null)
    {
      Color cl = color ?? Color.Transparent;
      for (int i = 0; i < Data.Length; i++)
      {
        Data[i] = cl;
      }

      SetData();
    }

    public Color GetPixel(int x, int y) => Data[y * Texture.Width + x];
    public void SetPixel(int x, int y, Color cl) => Data[y * Texture.Width + x] = cl;
    public void SetData() => Texture.SetData(Data);

    public void Render(Action<CUISpriteBatch> renderFunc)
    {
      CUICore.GraphicsDevice.SetRenderTarget(Texture);

      //TODO save and restore scissor rect
      SpriteBatch.Begin(samplerState: CUICore.SamplerState, rasterizerState: CUICore.RasterizerState);

      renderFunc(SpriteBatch);

      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);
    }



    public CUICanvas(int x, int y) : base()
    {
      BackgroundColor = Color.White;
      Size = new Point(x, y);
      SpriteBatch = CUISpriteBatch.Create();
    }

    public void Dispose() => Texture?.Dispose();
  }
}