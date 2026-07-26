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

        CUITexture2D texture = CUITexture2D.Create(256, 256).Fill((x, y) =>
        {
          return new Color((x * x + y * y) % 256, 0, 0);
        });

        frame["layout"].Background.Sprite = new CUISprite(texture);

        return frame;
      }
    }
  }
}