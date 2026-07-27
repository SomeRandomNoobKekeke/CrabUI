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


        float maxl = 50 * 50;
        Vector2 center = new Vector2(128, 128);
        CUITexture2D texture = CUITexture2D.Create(256, 256).Fill((v) =>
        {
          Vector2 diff = v - center;
          float l = diff.LengthSquared();



          if (l < maxl) return Color.Lime;
          return new Color(0, (int)(255 - (l - maxl) / 3.0f), 0);

        });

        CUICore.TextureManager.Add(texture, "ConstructedTexture");

        frame["layout"].Background.Sprite = new CUISprite(texture);

        return frame;
      }
    }
  }
}