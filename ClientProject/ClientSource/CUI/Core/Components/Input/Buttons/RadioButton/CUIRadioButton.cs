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
  public partial class CUIRadioButton : CUITextButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIRadioButton>((c) =>
    {
      c.TextColor = c.Palette["text"];
      c.MasterColor = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(2, 4, 2, 4);
      Background.Sprite = CUISprite.Vignette;
    }

    [CUISerializableProp]
    public Color OnColor { get; set; } = new Color(0, 255, 255);

    [CUISerializableProp]
    public Color OffColor { get; set; } = new Color(0, 0, 255);

    public RadioGroup Group { get; set; }
    public string GroupName
    {
      get => Group.Name;
      set => Group = RadioGroup.GetOrCreate(value);
    }

    private void HandleSelect()
    {
      Selected?.Invoke();
      Toggled?.Invoke(true);
      DetermineColor();
    }

    private void HandleDeselect()
    {
      Deselected?.Invoke();
      Toggled?.Invoke(false);
      DetermineColor();
    }

    public Action OnSelected { set { Selected += value; } }
    public event Action Selected;

    public Action OnDeselected { set { Deselected += value; } }
    public event Action Deselected;

    public Action<bool> OnToggled { set { Toggled += value; } }
    public event Action<bool> Toggled;

    public bool IsSelected
    {
      get => Group?.Current == this;
      set
      {
        if (value)
        {
          Group?.Select(this);
        }
        else
        {
          Group?.Deselect(this);
        }
      }
    }

    public void Select() => Group?.Select(this);
    public void Deselect() => Group?.Deselect(this);
    public void Toggle() => IsSelected = !IsSelected;

    public override Color MasterColor
    {
      set
      {
        OnColor = value.MultOpaque(0.7f);
        OffColor = value.MultOpaque(0.3f);
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      if (IsSelected)
      {
        Background.Color = OnColor;
        // if (MouseOver) Background.Color = OnColor.Multiply(2.0f);
        // if (MousePressed) Background.Color = OnColor.Multiply(3.0f);
      }
      else
      {
        Background.Color = OffColor;
        if (MouseOver) Background.Color = OffColor.Multiply(1.3f);
        // if (MousePressed) Background.Color = OffColor.Multiply(3.0f);
      }
    }

    public CUIRadioButton() : base()
    {
      MouseDown += (e) => Toggle();
    }
    public CUIRadioButton(string text) : this()
    {
      Text = text;
    }
  }
}