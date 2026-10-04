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
  public partial class CUISprite
  {
    private static Point IconsTL = new Point(462, 0);
    private static Point IconsSize = new Point(24, 24);
    public static CUISprite AtIconIndex(string name, int x, int y, int w = 1, int h = 1)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        Name = name,
        SourceRectangle = new Rectangle(
          IconsTL.X + 1 + (IconsSize.X + 2) * x,
          IconsTL.Y + 1 + (IconsSize.Y + 2) * y,
          IconsSize.X * w,
          IconsSize.Y * h
        )
      };

    /// <summary>
    /// In case you want to keep component resized to icon but also make the icon invisible
    /// </summary>
    public static CUISprite EmptyIcon => AtIconIndex("EmptyIcon", 0, 0);
    public static CUISprite CrossIcon => AtIconIndex("CrossIcon", 1, 0);
    public static CUISprite AngleLeftIcon => AtIconIndex("AngleLeftIcon", 2, 0);
    public static CUISprite AngleDownIcon => AtIconIndex("AngleDownIcon", 3, 0);
    public static CUISprite CheckIcon => AtIconIndex("CheckIcon", 4, 0);
  }
}