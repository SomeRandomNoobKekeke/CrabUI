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
    public class SolidTextBlock : CUITextBlock
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<SolidTextBlock>((c) =>
      {
        c.Background.Color = c.Palette["panel"];
      });
      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.DimmedVertical;
      }
      public SolidTextBlock() : base() { }
      public SolidTextBlock(string text) : base(text) { }
    }
  }
}