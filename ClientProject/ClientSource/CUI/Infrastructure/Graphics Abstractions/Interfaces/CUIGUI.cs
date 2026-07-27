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
  public interface CUIGUI
  {
    public SamplerState SamplerState { get; }

    //TODO It's actually in GameMain
    public RasterizerState RasterizerState { get; }

    public void DrawLine(CUISpriteBatch spriteBatch, Vector2 start, Vector2 end, float width, Color color);

  }
}