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
  public partial interface CUITexture2D : IDisposable
  {
    public static CUITexture2D White => __CUITexture2D.White;
    public static CUITexture2D BaroDev => CUICore.TextureManager.Get("BaroDev");

    //TODO there should be an abstract factory for this
    public static CUITexture2D Create(int width, int height, bool mipmap, SurfaceFormat format)
      => __CUITexture2D.Create(width, height, mipmap, format);

    public static CUITexture2D Create(int width, int height) => __CUITexture2D.Create(width, height);


    public string Key { get; set; }
    public Rectangle Bounds { get; }
    public Color[] Data { get; set; }

    public int Width { get; }
    public int Height { get; }

    public float TexelWidth { get; }
    public float TexelHeight { get; }

    public void SetData(Color[] data);
    public void SetData(int level, int arraySlice, Rectangle? rect, Color[] data, int startIndex, int elementCount);
    public void SetData(int level, Rectangle? rect, Color[] data, int startIndex, int elementCount);
    public void SetData(Color[] data, int startIndex, int elementCount);


    public void GetData(int level, int arraySlice, Rectangle? rect, Color[] data, int startIndex, int elementCount);
    public void GetData(int level, Rectangle? rect, Color[] data, int startIndex, int elementCount);
    public void GetData(Color[] data);
    public void GetData(Color[] data, int startIndex, int elementCount);

    public CUITexture2D Fill(Func<int, int, Color> fillFunc);
  }

}