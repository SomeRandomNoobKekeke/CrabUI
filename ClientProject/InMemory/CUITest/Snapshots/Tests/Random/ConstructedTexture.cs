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

        CUITexture2D texture2 = CUITexture2D.Create(100, 100);
        texture2.Dispose();

        CUITexture2D texture = new TextureBuilder(256, 256)
          .Load("BaroDev")
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
          .Blur(0.002f)
          .DrawRingSector(new RingSegmentParams()
          {
            Origin = new Vector2(200, 150),
            Hole = 20,
            Height = 30,
            Angle = Math.PI * 0.3,
            AngleOffset = 1.5,
            FillColor = Color.Lime,
          })
          .DrawCircle(new Vector2(100, 100), 30, Color.Red * 0.5f)
          .DrawRing(new Vector2(100, 100), 30, Color.Red)


          .Build();

        CUICore.TextureManager.Add(texture, "ConstructedTexture");

        frame["layout"].Background.Sprite = new CUISprite(texture);

        return frame;
      }
    }
  }
}