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

    //=======================>  1  <=======================
    public static CUISprite FloppyDiscIcon => AtIconIndex("FloppyDiscIcon", 0, 1);
    public static CUISprite DownloadIcon => AtIconIndex("DownloadIcon", 1, 1);
    public static CUISprite UploadIcon => AtIconIndex("UploadIcon", 2, 1);
    public static CUISprite LockIcon => AtIconIndex("LockIcon", 3, 1);
    public static CUISprite UnlockIcon => AtIconIndex("UnlockIcon", 4, 1);
    public static CUISprite BugIcon => AtIconIndex("BugIcon", 5, 1);
    public static CUISprite ShieldIcon => AtIconIndex("ShieldIcon", 6, 1);
    public static CUISprite ShieldSolidIcon => AtIconIndex("ShieldSolidIcon", 7, 1);
    public static CUISprite FlagIcon => AtIconIndex("FlagIcon", 8, 1);
    public static CUISprite EyeIcon => AtIconIndex("EyeIcon", 9, 1);
    public static CUISprite ClosedEyeIcon => AtIconIndex("ClosedEyeIcon", 10, 1);

    //=======================>  2  <=======================
    public static CUISprite TrashCanIcon => AtIconIndex("TrashCanIcon", 0, 2);
    public static CUISprite GearIcon => AtIconIndex("GearIcon", 1, 2);
    public static CUISprite HomeIcon => AtIconIndex("HomeIcon", 2, 2);
    public static CUISprite RoundArrowIcon => AtIconIndex("RoundArrowIcon", 3, 2);
    public static CUISprite BrushAndPencilIcon => AtIconIndex("BrushAndPencilIcon", 4, 2);
    public static CUISprite BrushAndPaletteIcon => AtIconIndex("BrushAndPaletteIcon", 5, 2);
    public static CUISprite TargetIcon => AtIconIndex("TargetIcon", 6, 2);
    public static CUISprite BombIcon => AtIconIndex("BombIcon", 7, 2);
    public static CUISprite ExplosionIcon => AtIconIndex("ExplosionIcon", 8, 2);
    public static CUISprite FireIcon => AtIconIndex("FireIcon", 9, 2);
    public static CUISprite ShowflakeIcon => AtIconIndex("ShowflakeIcon", 10, 2);

    //=======================>  3  <=======================
    public static CUISprite MoveFreeIcon => AtIconIndex("MoveFreeIcon", 0, 3);
    public static CUISprite MinimizeIcon => AtIconIndex("MinimizeIcon", 1, 3);
    public static CUISprite MaximizeIcon => AtIconIndex("MaximizeIcon", 2, 3);
    public static CUISprite MoveVerticalIcon => AtIconIndex("MoveVerticalIcon", 3, 3);
    public static CUISprite MoveHorizontalIcon => AtIconIndex("MoveHorizontalIcon", 4, 3);
    public static CUISprite ScullIcon => AtIconIndex("ScullIcon", 5, 3);
    public static CUISprite BiohazardIcon => AtIconIndex("BiohazardIcon", 6, 3);
    public static CUISprite RadiationIcon => AtIconIndex("RadiationIcon", 7, 3);
    public static CUISprite ExclamationMarkIcon => AtIconIndex("ExclamationMarkIcon", 8, 3);
    public static CUISprite WarningRoundIcon => AtIconIndex("WarningRoundIcon", 9, 3);
    public static CUISprite WarningTriangleIcon => AtIconIndex("WarningTriangleIcon", 10, 3);
    public static CUISprite WrenchIcon => AtIconIndex("WrenchIcon", 11, 3);

    //=======================>  3  <=======================
    public static CUISprite PlayIcon => AtIconIndex("PlayIcon", 0, 4);
    public static CUISprite PauseIcon => AtIconIndex("PauseIcon", 1, 4);
    public static CUISprite RewindIcon => AtIconIndex("RewindIcon", 2, 4);
    public static CUISprite RestartIcon => AtIconIndex("RestartIcon", 3, 4);
    public static CUISprite MagnifyingGlassIcon => AtIconIndex("MagnifyingGlassIcon", 4, 4);
    public static CUISprite ZoomInIcon => AtIconIndex("ZoomInIcon", 5, 4);
    public static CUISprite ZoomOutIcon => AtIconIndex("ZoomOutIcon", 6, 4);
    public static CUISprite QuestionIcon => AtIconIndex("QuestionIcon", 7, 4);
    public static CUISprite InfoIcon => AtIconIndex("InfoIcon", 8, 4);
    public static CUISprite BarsIcon => AtIconIndex("BarsIcon", 9, 4);
    public static CUISprite SettingsIcon => AtIconIndex("SettingsIcon", 10, 4);
    public static CUISprite MainIcon => AtIconIndex("MainIcon", 11, 4);
    public static CUISprite ShareIcon => AtIconIndex("ShareIcon", 12, 4);
    public static CUISprite CalcIcon => AtIconIndex("CalcIcon", 13, 4);

    //=======================>  5  <=======================
    public static CUISprite BookmarkOnIcon => AtIconIndex("BookmarkOnIcon", 0, 5);
    public static CUISprite BookmarkOffIcon => AtIconIndex("BookmarkOffIcon", 1, 5);
    public static CUISprite StarOnIcon => AtIconIndex("StarOnIcon", 2, 5);
    public static CUISprite StarOffIcon => AtIconIndex("StarOffIcon", 3, 5);
    public static CUISprite FolderIcon => AtIconIndex("FolderIcon", 4, 5);
    public static CUISprite ExitIcon => AtIconIndex("ExitIcon", 5, 5);
    public static CUISprite PencilIcon => AtIconIndex("PencilIcon", 6, 5);
    public static CUISprite ScissorsIcon => AtIconIndex("ScissorsIcon", 7, 5);
    public static CUISprite FilterIcon => AtIconIndex("FilterIcon", 8, 5);
    public static CUISprite DocumentIcon => AtIconIndex("DocumentIcon", 9, 5);
    public static CUISprite CopyIcon => AtIconIndex("CopyIcon", 10, 5);
    public static CUISprite LoadingIcon => AtIconIndex("LoadingIcon", 11, 5);
    public static CUISprite PinIcon => AtIconIndex("PinIcon", 12, 5);

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