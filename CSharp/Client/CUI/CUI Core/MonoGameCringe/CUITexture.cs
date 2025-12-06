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
  public class CUITexture : ICUITexture
  {
    public static CUITexture White { get; } = new CUITexture(GUI.WhiteTexture);


    public Texture2D Texture;

    public void Use(Texture2D texture) => Texture = texture;
    public CUITexture(Texture2D texture) => Texture = texture;
  }
}