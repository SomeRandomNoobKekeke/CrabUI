using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CursedUI
{
  public partial record CUISprite
  {
    private static Point IconsTL = new Point(462, 0);
    public static CUISprite AtIconIndex(int x, int y, int w = 1, int h = 1)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        SourceRectangle = new Rectangle(IconsTL.X + 1 + 26 * x, IconsTL.Y + 1 + 26 * y, 24 * w, 24 * h)
      };

    /// <summary>
    /// In case you want to keep component resized to icon but also make the icon invisible
    /// </summary>
    public static CUISprite EmptyIcon => AtIconIndex(0, 0);
    public static CUISprite CrossIcon => AtIconIndex(1, 0);
    public static CUISprite AngleLeftIcon => AtIconIndex(2, 0);
    public static CUISprite AngleDownIcon => AtIconIndex(3, 0);
    public static CUISprite CheckIcon => AtIconIndex(4, 0);
  }
}