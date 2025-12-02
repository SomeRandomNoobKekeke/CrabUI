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
  public class CUISpriteBatch : ICUISpriteBatch
  {
    public SpriteBatch SpriteBatch;

    public void Use(SpriteBatch spriteBatch) => this.SpriteBatch = spriteBatch;
  }
}