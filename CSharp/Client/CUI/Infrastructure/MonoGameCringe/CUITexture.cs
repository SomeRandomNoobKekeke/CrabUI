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
  public class CUITexture2D(Texture2D texture)
  {
    public static CUITexture2D White { get; } = new CUITexture2D(GUI.WhiteTexture);
    public Texture2D? XNATexture { get; set; } = texture;


  }
}