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

        CUITexture2D texture2 = CUITexture2D.Create(100, 100);
        texture2.Dispose();

        CUITexture2D texture = new TextureBuilder(256, 256)
          .Load("BaroDev")
          .DrawCircle(new Vector2(100, 100), 30, Color.Red * 0.5f)
          .DrawRing(new Vector2(100, 100), 30, Color.Red)
          .Render(spritebatch =>
          {
            // CUICore.GUI.DrawLine(spritebatch, new Vector2(20, 20), new Vector2(40, 40), 10, Color.Lime);
            spritebatch.DrawLine(new Vector2(20, 20), new Vector2(40, 40), Color.Lime, 5);
            spritebatch.DrawCircle(new Vector2(50, 50), 30, 20, Color.Pink, 2);
            spritebatch.DrawPolygon(new Vector2(50, 50), new Polygon(new Vector2[]
            {
              new  Vector2(-20,-10),
              new  Vector2(-10,20),
              new  Vector2(10,20),
              new  Vector2(20,-10),
            }),
            Color.AliceBlue, 3);
          })
          .Damage()
          .Build();

        CUICore.TextureManager.Add(texture, "ConstructedTexture");

        frame["layout"].Background.Sprite = new CUISprite(texture);

        return frame;
      }
    }
  }
}