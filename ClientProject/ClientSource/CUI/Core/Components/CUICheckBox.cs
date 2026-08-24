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
  public partial class CUICheckBox : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUICheckBox>((c) =>
    {
      c.OnSprite.Color = c.Palette["main"];
      c.OffSprite.Color = Color.Lerp(c.Palette["main"], Color.Gray, 0.5f);
    });

    protected override void InitStyle()
    {
      base.InitStyle();

      OnSprite = CUISprite.AtPos(0, 1);
      OffSprite = CUISprite.AtPos(1, 1);

      State = false;

      Absolute = new CUINullRect(w: 24, h: 24);
    }

    public bool PlaySound { get; set; } = true;
    public GUISoundType ClickSound { get; set; } = GUISoundType.TickBox;//TODO don't reference it directly? there should be some cui sound manager

    public CUISprite OnSprite { get; set; }
    public CUISprite OffSprite { get; set; }


    public void Toggle() => State = !State;
    private bool _State; public bool State
    {
      get => _State;
      set
      {
        _State = value;
        Background.Sprite = value ? OnSprite : OffSprite;
      }
    }


    public CUICheckBox() : base()
    {
      MouseDown += (e) =>
      {
        Toggle();
        if (PlaySound) SoundPlayer.PlayUISound(ClickSound);
      };
      ConsumeMouseEvents = true;
    }
  }
}