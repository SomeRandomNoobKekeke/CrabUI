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
          Absolute = new CUINullRect(w: 256, h: 256),
        };

        CUITexture2D texture = new TextureBuilder(256, 256)
          .Load("BaroDev")
          .DrawCircle(new Vector2(100, 100), 30, Color.Red * 0.5f)
          .DrawCircle(new Vector2(100, 100), 30, Color.Red * 0.5f, Color.Yellow, 0, 2)
          .DrawPoint(new Vector2(150, 100), Color.Lime, 5, 5)
          .DrawLine(new Vector2(10, 30), new Vector2(100, 50), Color.Blue, 0.5f, 2)
          .DrawArc(new Vector2(150, 150), 50, -1.5 * Math.PI, 0.3 * Math.PI, Color.Yellow, 2, 2)
          .Build(tracked: true);

        CUICore.TextureManager.Add(texture, "ConstructedTexture");

        frame.Background.Sprite = new CUISprite(texture);
        frame.Absolute = new CUINullRect(w: frame.Background.Sprite.Texture.Width, h: frame.Background.Sprite.Texture.Height);


        return frame;
      }
    }
  }
}