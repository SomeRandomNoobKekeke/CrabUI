using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComponentGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{
  public partial class CUIButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIButton>((c) =>
    {
      c.MasterColor = Color.Blue;
    });

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
      BackgroundColor = InactiveColor;
      if (MouseOver) BackgroundColor = MouseOverColor;
      if (MousePressed) BackgroundColor = MousePressedColor;
    }




    public CUIButton() : base() { }
    public CUIButton(string text) : base(text) { }
  }
}