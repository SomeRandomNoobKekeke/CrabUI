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
  public partial class CUIRadioButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIRadioButton>((c) =>
    {
      c.TextColor = c.Palette["text"];
      c.MasterColor = c.Palette["main"];
    });

    [CUISerializableProp]
    public Color OnColor { get; set; } = new Color(0, 255, 255);

    [CUISerializableProp]
    public Color OffColor { get; set; } = new Color(0, 0, 255);

    private RadioMutex Mutex;
    public string Group
    {
      get => Mutex?.Name;
      set
      {
        if (Mutex != null)
        {
          Mutex.Selected -= HandleSelect;
          Mutex.Deselected -= HandleDeselect;
        }

        Mutex = new RadioMutex(value);
        Mutex.Selected += HandleSelect;
        Mutex.Deselected += HandleDeselect;
      }
    }

    private void HandleSelect()
    {
      Selected?.Invoke();
      DetermineColor();
    }

    private void HandleDeselect()
    {
      Deselected?.Invoke();
      DetermineColor();
    }

    public Action OnSelected { set { Selected += value; } }
    public event Action Selected;

    public Action OnDeselected { set { Deselected += value; } }
    public event Action Deselected;

    public bool IsSelected => Mutex?.IsSelected ?? false;
    public void Select() => Mutex?.Select();


    public override Color MasterColor
    {
      set
      {
        OnColor = value.Multiply(0.8f);
        OffColor = value.Multiply(0.3f);
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      if (IsSelected)
      {
        Background.Color = OnColor;
        if (MouseOver) Background.Color = OnColor.Multiply(2.0f);
        if (MousePressed) Background.Color = OnColor.Multiply(3.0f);
      }
      else
      {
        Background.Color = OffColor;
        if (MouseOver) Background.Color = OffColor.Multiply(2.0f);
        if (MousePressed) Background.Color = OffColor.Multiply(3.0f);
      }
    }

    public CUIRadioButton() : base()
    {
      MouseDown += (e) => Select();
    }
    public CUIRadioButton(string text) : this()
    {
      Text = text;
    }
  }
}