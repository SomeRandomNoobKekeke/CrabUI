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
          .DrawRectangle(20, 190, 30, 30, Color.Green * 0.5f)
          .DrawCircle(new Vector2(100, 100), 30, Color.Red * 0.5f)
          .DrawCircle(new Vector2(100, 100), 30, Color.Red * 0.5f, Color.Yellow, 0.5f, 2)
          .DrawPoint(new Vector2(150, 100), Color.Lime, 5, 5)
          .DrawLine(new Vector2(10, 30), new Vector2(100, 50), Color.Blue, 0.5f, 2)
          .DrawArc(new Vector2(150, 150), 50, -1.5 * Math.PI, 0.3 * Math.PI, Color.Yellow, 0.5f, 2)
          .Render(sb =>
          {
            // CUICore.GUI.DrawLine(spritebatch, new Vector2(20, 20), new Vector2(40, 40), 10, Color.Lime);
            sb.DrawLine(new Vector2(20, 20), new Vector2(40, 40), Color.Lime, 5);
            sb.DrawCircle(new Vector2(50, 50), 30, 20, Color.Pink, 2);
            sb.DrawPolygon(new Vector2(50, 50), new Polygon(new Vector2[]
            {
              new  Vector2(-20,-10),
              new  Vector2(-10,20),
              new  Vector2(10,20),
              new  Vector2(20,-10),
            }),
            Color.AliceBlue, 3);
          })
          .Damage()
          .DrawRingSector(new Vector2(310, 100), 20, 70, -Math.PI / 4.0, Math.PI / 4.0, Color.Green, Color.Lime, 0.5f, 2)
          .DrawRingSector(new Vector2(300, 110), 20, 70, -Math.PI / 4.0 + Math.PI / 2, Math.PI / 4.0 + Math.PI / 2, Color.Green, Color.Lime, 0.5f, 2)
          .DrawRingSector(new Vector2(290, 100), 20, 70, -Math.PI / 4.0 + Math.PI, Math.PI / 4.0 + Math.PI, Color.Green, Color.Lime, 0.5f, 2)
          .DrawRingSector(new Vector2(300, 90), 20, 70, -Math.PI / 4.0 + 3 * Math.PI / 2, Math.PI / 4.0 + 3 * Math.PI / 2, Color.Green * 0.5f, Color.Lime * 0.5f, 0.5f, 2)

          .Build(tracked: true);

        CUICore.TextureManager.Add("ConstructedTexture", texture);

        frame.Background.Sprite = new CUISprite(texture);
        frame.Absolute = new CUINullRect(w: frame.Background.Sprite.Texture.Width, h: frame.Background.Sprite.Texture.Height);


        return frame;
      }
    }
  }
}