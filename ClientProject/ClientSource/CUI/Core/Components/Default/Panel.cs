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
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<Panel>((c) =>
      {
        c.Background.Color = c.Palette["panel"];
      });
    }

    public class HorizontalPanel : CUIHorizontalList
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<HorizontalPanel>((c) =>
      {
        c.Background.Color = c.Palette["panel"];

      });

      protected override void InitStyle()
      {
        base.InitStyle();
        FitContent = new CUIBool2(false, true);
        Borders = new CUISizes(0, 0, 1, 0);
      }
    }

    public class VerticalPanel : CUIVerticalList
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<VerticalPanel>((c) =>
      {
        c.Background.Color = c.Palette["panel"];
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        FitContent = new CUIBool2(false, true);
      }
    }


    public class BackPanel : CUIComponent
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<BackPanel>((c) =>
      {
        c.Background.Color = c.Palette["backpanel"];
      });
    }

    public class HorizontalBackPanel : CUIHorizontalList
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<HorizontalBackPanel>((c) =>
      {
        c.Background.Color = c.Palette["backpanel"];
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        FitContent = new CUIBool2(false, true);
        Borders = new CUISizes(0, 0, 1, 0);
      }
    }

    public class VerticalBackPanel : CUIVerticalList
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<VerticalBackPanel>((c) =>
      {
        c.Background.Color = c.Palette["backpanel"];
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        FitContent = new CUIBool2(false, true);
      }
    }
  }
}