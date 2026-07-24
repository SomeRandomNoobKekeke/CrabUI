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
  public partial class CUIToggleButton : CUIButtonBase, IComponent
  {

    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIToggleButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
    });

    [CUISerializableProp]
    public Color OnColor { get; set; } = new Color(255, 0, 255);

    [CUISerializableProp]
    public Color OffColorHovered { get; set; } = new Color(64, 0, 64);

    [CUISerializableProp]
    public Color OffColor { get; set; } = new Color(48, 0, 48);


    private bool _State; public bool State
    {
      get => _State;
      set
      {
        _State = value;
        DetermineColor();
      }
    }

    public override Color MasterColor
    {
      set
      {
        OnColor = value.MultOpaque(1.0f);
        OffColorHovered = value.MultOpaque(0.25f);
        OffColor = OffColorHovered.MultOpaque(0.7f);
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      if (State)
      {
        Background.Color = OnColor;
      }
      else
      {
        Background.Color = OffColor;
        if (MouseOver) Background.Color = OffColorHovered;
      }
    }

    public Action<bool> OnToggle { set { Toggle += value; } }
    public event Action<bool> Toggle;

    public CUIToggleButton() : base()
    {
      MouseDown += (c, e) =>
      {
        State = !State;
        Toggle?.Invoke(State);
      };
    }
    public CUIToggleButton(string text) : this()
    {
      Text = text;
    }
  }
}