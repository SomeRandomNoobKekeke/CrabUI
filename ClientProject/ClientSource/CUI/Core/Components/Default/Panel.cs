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

namespace CrabUI
{
  public static partial class CUIDefault
  {
    public class HorizontalPanel : CUIHorizontalList
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<HorizontalPanel>((c) =>
      {
        c.Background.Color = c.Palette["main"].MultOpaque(0.4f);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.DimmedVerticalLight;
      }
    }

    public class VerticalPanel : CUIVerticalList
    {
      public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<VerticalPanel>((c) =>
      {
        c.Background.Color = c.Palette["main"].MultOpaque(0.4f);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.DimmedHorizontalLight;
      }
    }
  }
}