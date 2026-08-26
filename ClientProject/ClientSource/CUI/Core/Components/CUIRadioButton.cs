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
  public partial class CUIRadioButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIRadioButton>((c) =>
    {
      c.TextColor = c.Palette["text"];
      c.MasterColor = c.Palette["main"];
      c.Padding = new(2, 4, 2, 4);
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
      OnSelected?.Invoke();
      Changed?.Invoke(true);
      DetermineColor();
    }

    private void HandleDeselect()
    {
      OnDeselected?.Invoke();
      Changed?.Invoke(false);
      DetermineColor();
    }

    public Action AddOnSelected { set { OnSelected += value; } }
    public event Action OnSelected;

    public Action AddOnDeselected { set { OnDeselected += value; } }
    public event Action OnDeselected;

    public Action<bool> OnChanged { set { Changed += value; } }
    public event Action<bool> Changed;

    public bool Selected
    {
      get => Mutex?.IsSelected ?? false;
      set
      {
        if (value) Select();
      }
    }

    public void Select() => Mutex?.Select();


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
      if (Selected)
      {
        Background.Color = OnColor;
        // if (MouseOver) Background.Color = OnColor.Multiply(2.0f);
        // if (MousePressed) Background.Color = OnColor.Multiply(3.0f);
      }
      else
      {
        Background.Color = OffColor;
        if (MouseOver) Background.Color = OffColor.Multiply(2.0f);
        // if (MousePressed) Background.Color = OffColor.Multiply(3.0f);
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