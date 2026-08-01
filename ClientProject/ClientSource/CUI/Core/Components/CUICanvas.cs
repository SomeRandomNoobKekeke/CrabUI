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
    private Color[] _Data; public Color[] Data
    {
      get => _Data;
      set
      {
        _Data = value;
        Texture.SetData(_Data);
      }
    }
    public CUIRenderTarget2D Texture { get; private set; }
    public CUISpriteBatch SpriteBatch { get; private set; }


    public virtual Point Size
    {
      get => new Point(Texture.Width, Texture.Height);
      set
      {
        if (value.X == Texture?.Width && value.Y == Texture?.Height) return;

        CUIRenderTarget2D oldTexture = Texture;
        Texture = CUICore.TextureManager.CreateNewRenderTarget(value.X, value.Y);
        Data = new Color[Texture.Width * Texture.Height];

        Background.Sprite.Texture = Texture;
        Background.Sprite.SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

        oldTexture?.Dispose();
      }
    }

    public void Clear(Color? color = null)
    {
      Color cl = color ?? Color.Transparent;
      for (int i = 0; i < _Data.Length; i++)
      {
        _Data[i] = cl;
      }

      Texture.SetData(_Data);
    }

    public Color GetPixel(int x, int y) => Data[y * Texture.Width + x];
    public void SetPixel(int x, int y, Color cl) => _Data[y * Texture.Width + x] = cl;
    public void ApplyData() => Texture.SetData(_Data);

    public void Render(Action<CUISpriteBatch> renderFunc)
    {
      CUICore.GraphicsDevice.SetRenderTarget(Texture);

      //TODO save and restore scissor rect
      SpriteBatch.Begin(samplerState: CUICore.SamplerState, rasterizerState: CUICore.RasterizerState);

      renderFunc(SpriteBatch);

      SpriteBatch.End();

      CUICore.GraphicsDevice.SetRenderTarget(null);
    }


    public CUICanvas() : base()
    {
      Size = new Point(1, 1);
      Background.Color = Color.White;
      SpriteBatch = CUISpriteBatch.Create();
    }
    public CUICanvas(int x, int y) : this()
    {
      Size = new Point(x, y);
    }

    public virtual void Dispose() => Texture?.ForgetAndDispose();
  }
}