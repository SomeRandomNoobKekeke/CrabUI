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
  public partial class CUIButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
      c.TextColor = c.Palette["text"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(4, 2, 4, 2);
      Background.Sprite = CUISprite.VignetteLight;
    }

    [CUISerializableProp]
    public Color MouseOverColor { get; set; } = new Color(0, 0, 140);
    [CUISerializableProp]
    public Color MousePressedColor { get; set; } = new Color(0, 0, 200);
    [CUISerializableProp]
    public Color InactiveColor { get; set; } = new Color(0, 0, 100);

    public override Color MasterColor
    {
      set
      {
        InactiveColor = value.MultOpaque(0.8f);
        MouseOverColor = value.MultOpaque(0.9f);
        MousePressedColor = value;
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      Background.Color = InactiveColor;
      if (MouseOver) Background.Color = MouseOverColor;
      if (MousePressed) Background.Color = MousePressedColor;
    }

    public CUIButton() : base() { }
    public CUIButton(string text) : base(text) { }
  }
}