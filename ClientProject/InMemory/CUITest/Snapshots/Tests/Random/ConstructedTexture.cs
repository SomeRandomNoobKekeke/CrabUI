using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {

      public static CUIVisualComponent ConstructedTexture()
      {
        CUIFrame frame = new CUIDefault.Frame("ConstructedTexture")
        {
          Absolute = new CUINullRect(w: 512, h: 512),
        };

        CUITexture2D texture = new TextureBuilder(256, 256)
          .DrawCircle(new Vector2(100, 100), 30, Color.Red)
          .DrawRadialGradient(new Vector2(100, 100), 30, 35, Color.White, Color.Transparent)
          .DrawRadialGradient(new Vector2(100, 100), 25, 30, Color.Red, Color.White)
          .Render(spritebatch =>
          {
            CUICore.GUI.DrawLine(spritebatch, new Vector2(20, 20), new Vector2(40, 40), 10, Color.Lime);
          })
          .Build();

        CUICore.TextureManager.Add(texture, "ConstructedTexture");

        frame["layout"].Background.Sprite = new CUISprite(texture);

        return frame;
      }
    }
  }
}