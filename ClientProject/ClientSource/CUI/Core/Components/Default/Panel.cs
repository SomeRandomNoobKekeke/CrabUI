using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CursedUI
{
  public static partial class CUIDefault
  {
    public class Panel : CUIComponent
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<Panel>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.4f);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        // Background.Sprite = CUISprite.Vignette;
      }
    }

    public class HorizontalPanel : CUIHorizontalList
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<HorizontalPanel>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.4f);
        c.FitContent = new CUIBool2(false, true);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        // Background.Sprite = CUISprite.DimmedVerticalLight;
      }
    }

    public class VerticalPanel : CUIVerticalList
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<VerticalPanel>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.4f);
        c.FitContent = new CUIBool2(true, false);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        // Background.Sprite = CUISprite.DimmedHorizontalLight;
      }
    }


    public class BackPanel : CUIComponent
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<BackPanel>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.2f);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        // Background.Sprite = CUISprite.Vignette;
      }
    }

    public class HorizontalBackPanel : CUIHorizontalList
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<HorizontalBackPanel>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.2f);
        c.FitContent = new CUIBool2(false, true);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        // Background.Sprite = CUISprite.DimmedVerticalLight;
      }
    }

    public class VerticalBackPanel : CUIVerticalList
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<VerticalBackPanel>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.2f);
        c.FitContent = new CUIBool2(true, false);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        // Background.Sprite = CUISprite.DimmedHorizontalLight;
      }
    }
  }
}