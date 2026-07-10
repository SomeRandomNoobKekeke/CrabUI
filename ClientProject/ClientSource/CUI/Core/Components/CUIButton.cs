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
      c.MasterColor = c.Palette.Colors["button"];
      c.TextColor = c.Palette.Colors["text"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(4, 2, 4, 2);
    }

    [CUISerializable]
    public Color MouseOverColor { get; set; } = new Color(0, 0, 140);
    [CUISerializable]
    public Color MousePressedColor { get; set; } = new Color(0, 0, 200);
    [CUISerializable]
    public Color InactiveColor { get; set; } = new Color(0, 0, 100);

    public override Color MasterColor
    {
      set
      {
        InactiveColor = new Color((int)(value.R * 0.7f), (int)(value.G * 0.7f), (int)(value.B * 0.7f), value.A);
        MouseOverColor = new Color((int)(value.R * 0.9f), (int)(value.G * 0.9f), (int)(value.B * 0.9f), value.A);
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


    public void Click()
    {
      Events.MouseDown.Raise(
        this,
        new CUIMouseDownEvent(CUIMouseButton.LeftButton, CUICore.Input.Mouse)
      );
    }



    public CUIButton() : base() { }
    public CUIButton(string text) : base(text) { }
  }
}