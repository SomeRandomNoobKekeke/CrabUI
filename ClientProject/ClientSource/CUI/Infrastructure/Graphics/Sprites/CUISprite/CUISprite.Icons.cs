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

    //=======================>  0  <=======================
    /// <summary>
    /// In case you want to keep component resized to icon but also make the icon invisible
    /// </summary>
    public static CUISprite EmptyIcon => AtIconIndex("EmptyIcon", 0, 0);
    public static CUISprite CrossIcon => AtIconIndex("CrossIcon", 1, 0);
    public static CUISprite CheckIcon => AtIconIndex("CheckIcon", 2, 0);
    public static CUISprite PlusIcon => AtIconIndex("PlusIcon", 3, 0);
    public static CUISprite MinusIcon => AtIconIndex("MinusIcon", 4, 0);
    public static CUISprite CheckOffIcon => AtIconIndex("CheckOffIcon", 5, 0);
    public static CUISprite CheckOnIcon => AtIconIndex("CheckOnIcon", 6, 0);

    //=======================>  2  <=======================
    public static CUISprite TrashCanIcon => AtIconIndex("TrashCanIcon", 0, 2);
    public static CUISprite GearIcon => AtIconIndex("GearIcon", 1, 2);
    public static CUISprite HomeIcon => AtIconIndex("HomeIcon", 2, 2);
    public static CUISprite RoundArrowIcon => AtIconIndex("RoundArrowIcon", 3, 2);
    public static CUISprite BrushAndPencilIcon => AtIconIndex("BrushAndPencilIcon", 4, 2);
    public static CUISprite BrushAndPaletteIcon => AtIconIndex("BrushAndPaletteIcon", 5, 2);

    //=======================>  5  <=======================
    public static CUISprite BookmarkOnIcon => AtIconIndex("BookmarkOnIcon", 0, 5);
    public static CUISprite BookmarkOffIcon => AtIconIndex("BookmarkOffIcon", 1, 5);
    public static CUISprite StarOnIcon => AtIconIndex("StarOnIcon", 2, 5);
    public static CUISprite StarOffIcon => AtIconIndex("StarOffIcon", 3, 5);

    //=======================>  6  <=======================
    public static CUISprite AngleUpIcon => AtIconIndex("AngleUpIcon", 0, 6);
    public static CUISprite AngleRightIcon => AtIconIndex("AngleRightIcon", 1, 6);
    public static CUISprite AngleDownIcon => AtIconIndex("AngleDownIcon", 2, 6);
    public static CUISprite AngleLeftIcon => AtIconIndex("AngleLeftIcon", 3, 6);
    public static CUISprite ArrowUpIcon => AtIconIndex("ArrowUpIcon", 4, 6);
    public static CUISprite ArrowRightIcon => AtIconIndex("ArrowRightIcon", 5, 6);
    public static CUISprite ArrowDownIcon => AtIconIndex("ArrowDownIcon", 6, 6);
    public static CUISprite ArrowLeftIcon => AtIconIndex("ArrowLeftIcon", 7, 6);
    public static CUISprite ArrowSlimUpIcon => AtIconIndex("ArrowSlimUpIcon", 8, 6);
    public static CUISprite ArrowSlimRightIcon => AtIconIndex("ArrowSlimRightIcon", 9, 6);
    public static CUISprite ArrowSlimDownIcon => AtIconIndex("ArrowSlimDownIcon", 10, 6);
    public static CUISprite ArrowSlimLeftIcon => AtIconIndex("ArrowSlimLeftIcon", 11, 6);

    //=======================>  7  <=======================
    public static CUISprite AttachToTopIcon => AtIconIndex("AttachToTopIcon", 0, 7);
    public static CUISprite AttachToRightIcon => AtIconIndex("AttachToRightIcon", 1, 7);
    public static CUISprite AttachToBottomIcon => AtIconIndex("AttachToBottomIcon", 2, 7);
    public static CUISprite AttachToLeftIcon => AtIconIndex("AttachToBottomIcon", 3, 7);
    public static CUISprite AttachToCenterIcon => AtIconIndex("AttachToCenterIcon", 4, 7);
    public static CUISprite TopSideIcon => AtIconIndex("TopSideIcon", 5, 7);
    public static CUISprite RightSideIcon => AtIconIndex("RightSideIcon", 6, 7);
    public static CUISprite BottomSideIcon => AtIconIndex("BottomSideIcon", 7, 7);
    public static CUISprite LeftSideIcon => AtIconIndex("LeftSideIcon", 8, 7);

    //=======================>  8  <=======================
    public static CUISprite SpeedometerIcon => AtIconIndex("SpeedometerIcon", 0, 8);
    public static CUISprite Speedometer2Icon => AtIconIndex("Speedometer2Icon", 1, 8);
    public static CUISprite HistoryClockIcon => AtIconIndex("HistoryClockIcon", 2, 8);
    public static CUISprite AlarmIcon => AtIconIndex("AlarmIcon", 3, 8);
    public static CUISprite FastTimerIcon => AtIconIndex("FastTimerIcon", 4, 8);
    public static CUISprite TimerIcon => AtIconIndex("TimerIcon", 5, 8);
    public static CUISprite BurningTimerIcon => AtIconIndex("BurningTimerIcon", 6, 8);
    public static CUISprite HourglassIcon => AtIconIndex("HourglassIcon", 7, 8);
    public static CUISprite ClockIcon => AtIconIndex("ClockIcon", 8, 8);
  }
}