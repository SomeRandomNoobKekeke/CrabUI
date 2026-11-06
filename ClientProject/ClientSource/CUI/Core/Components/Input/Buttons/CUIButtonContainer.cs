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
  public partial class CUIButtonContainer : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<CUIButtonContainer>((c) =>
    {
      c.MasterColor = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Background.Sprite = CUISprite.Vignette;
    }

    public new Action<CUIButtonContainer> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIButtonContainer>("personal", value);
    }

    [CUISerializableProp]
    public Color MouseOverColor { get; set; }

    [CUISerializableProp]
    public Color MousePressedColor { get; set; }

    [CUISerializableProp]
    public Color InactiveColor { get; set; }


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



    public Action OnClick { set { Click += value; } }
    public event Action Click;


    public override void DetermineColor()
    {
      if (Disabled)
      {
        Background.Color = Palette["disabled"];
        return;
      }

      Background.Color = InactiveColor;
      if (MouseOver) Background.Color = MouseOverColor;
      if (MousePressed) Background.Color = MousePressedColor;
    }

    public CUIButtonContainer() : base()
    {
      MouseOff += (e) => DetermineColor();
      MouseOn += (e) => DetermineColor();
      DetermineColor();

      MouseDown += (e) =>
      {
        if (Disabled) return;
        if (!e.Mouse.M1.Down) return;
        if (PlaySound) SoundPlayer.PlayUISound(ClickSound);
        if (Emit != null) Commands.SendUp(Emit);
        Click?.Invoke();
      };

      ConsumeMouseEvents = true;
    }
  }
}