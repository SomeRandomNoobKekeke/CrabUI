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
    public class GlowingButton : CUIButton
    {
      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.VerticalGlowSolid;
      }
      public GlowingButton() : base() { }
      public GlowingButton(string text) : base(text) { }
    }
    public class GlowingToggleButton : CUIToggleButton
    {
      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.VerticalGlowSolid;
      }
      public GlowingToggleButton() : base() { }
      public GlowingToggleButton(string text) : base(text) { }
    }

    public class GlowingRadioButton : CUIRadioButton
    {
      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.VerticalGlowSolid;
      }
      public GlowingRadioButton() : base() { }
      public GlowingRadioButton(string text) : base(text) { }
    }
  }
}