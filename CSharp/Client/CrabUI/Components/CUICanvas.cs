using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{

  /// <summary>
  /// Allows you to manipulate pixel data of its texture
  /// </summary>
  public class CUICanvas : CUIComponent, IDisposable
  {
    public Color[] Data;

    public Texture2D Texture;


    /// <summary>
    /// Size of the internal texture
    /// Will automatically resize the texture and data array of set 
    /// </summary>
    public virtual Point Size
    {
      get => new Point(Texture.Width, Texture.Height);
      set
      {
        if (value.X == Texture?.Width && value.Y == Texture?.Height) return;

        Texture2D oldTexture = Texture;
        Texture = new Texture2D(GameMain.Instance.GraphicsDevice, value.X, value.Y);
        Data = new Color[Texture.Width * Texture.Height];
        BackgroundSprite = new CUISprite(Texture);
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

    public Color GetPixel(int x, int y)
    {
      return Data[y * Texture.Width + x];
    }

    public void SetPixel(int x, int y, Color cl)
    {
      Data[y * Texture.Width + x] = cl;
    }

    /// <summary>
    /// Call this method to transfer values from Data array into texture
    /// </summary>
    public void SetData()
    {
      Texture.SetData<Color>(Data);
    }

    public CUICanvas(int x, int y) : base()
    {
      Size = new Point(x, y);
      BackgroundColor = Color.White;
      BorderColor = Color.Black;
    }

    public CUICanvas() : this(100, 100)
    {

    }

    public void Dispose()
    {
      Texture?.Dispose();
    }
  }
}