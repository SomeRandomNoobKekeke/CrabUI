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
    public class ToggleButtonField : HorizontalBackPanel
    {
      public CUIToggleButton Button { get; }
      public CUITextBlock Label { get; }

      public string ButtonText
      {
        get => Button.Text;
        set => Button.Text = value;
      }

      public string LabelText
      {
        get => Label.Text;
        set => Label.Text = value;
      }

      public bool State
      {
        get => Button.State;
        set => Button.State = value;
      }

      public Action<bool> OnToggle { set { Button.OnToggle = value; } }
      public event Action<bool>? Toggle
      {
        add => Button.Toggle += value;
        remove => Button.Toggle -= value;
      }

      public ToggleButtonField() : base()
      {
        this["button"] = Button = new CUIToggleButton("Click");
        this["label"] = Label = new SolidTextBlock("do something funny") { Flex = 1 };
      }
    }
  }
}