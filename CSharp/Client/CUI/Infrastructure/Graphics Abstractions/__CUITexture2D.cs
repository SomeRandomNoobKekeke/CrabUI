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
    public static __CUITexture2D White = new __CUITexture2D(GUI.WhiteTexture);
    public Texture2D XNATexture { get; set; }

    public int Width => XNATexture.Width;
    public int Height => XNATexture.Height;

    public void SetData(Color[] data) => XNATexture.SetData<Color>(data);

    public __CUITexture2D(Texture2D texture) => XNATexture = texture;
    public __CUITexture2D() { }

    public void Dispose() => XNATexture.Dispose();
  }
}