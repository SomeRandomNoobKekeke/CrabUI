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
  public class __CUITexture2D : CUITexture2D, IDisposable
  {
    public static __CUITexture2D Create(int width, int height, bool mipmap, SurfaceFormat format)
      => new __CUITexture2D(width, height, mipmap, format);
    public static __CUITexture2D Create(int width, int height)
      => new __CUITexture2D(width, height, false, GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferFormat);

    public static __CUITexture2D White = new __CUITexture2D(GUI.WhiteTexture)
    {
      ShouldBeDisposed = false,
    };
    public Texture2D XNATexture { get; set; }

    public int Width => XNATexture.Width;
    public int Height => XNATexture.Height;

    public bool ShouldBeDisposed { get; set; } = true; //BRUH sneaky

    public void SetData(Color[] data) => XNATexture.SetData<Color>(data);
    public void SetData(int level, int arraySlice, Rectangle? rect, Color[] data, int startIndex, int elementCount)
      => XNATexture.SetData<Color>(level, arraySlice, rect, data, startIndex, elementCount);
    public void SetData(int level, Rectangle? rect, Color[] data, int startIndex, int elementCount)
      => XNATexture.SetData<Color>(level, rect, data, startIndex, elementCount);
    public void SetData(Color[] data, int startIndex, int elementCount)
      => XNATexture.SetData<Color>(data, startIndex, elementCount);


    public void GetData(int level, int arraySlice, Rectangle? rect, Color[] data, int startIndex, int elementCount)
      => XNATexture.GetData<Color>(level, arraySlice, rect, data, startIndex, elementCount);
    public void GetData(int level, Rectangle? rect, Color[] data, int startIndex, int elementCount)
      => XNATexture.GetData<Color>(level, rect, data, startIndex, elementCount);
    public void GetData(Color[] data) => XNATexture.GetData<Color>(data);
    public void GetData(Color[] data, int startIndex, int elementCount)
      => XNATexture.GetData<Color>(data, startIndex, elementCount);


    public __CUITexture2D(int width, int height, bool mipmap, SurfaceFormat format)
    {
      XNATexture = new Texture2D(GameMain.Instance.GraphicsDevice, width, height, mipmap, format);
    }
    public __CUITexture2D(Texture2D texture) => XNATexture = texture;
    public __CUITexture2D() { }

    public void Dispose()
    {
      if (ShouldBeDisposed) XNATexture.Dispose();
    }
  }
}