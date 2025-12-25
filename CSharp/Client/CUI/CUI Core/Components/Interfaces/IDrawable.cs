using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public interface IDrawable
  {
    public Rectangle DrawRect { get; set; }
    public void Draw(CUISpriteBatch spriteBatch);
  }
}