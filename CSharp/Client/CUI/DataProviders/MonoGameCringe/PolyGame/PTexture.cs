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
  public class PTexture(Texture2D texture)
  {
    public Texture2D Texture { get; } = texture;
    public static PTexture White { get; } = new PTexture(GUI.WhiteTexture);
  }
}