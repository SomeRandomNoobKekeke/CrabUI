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
    public class ButtonField : HorizontalBackPanel
    {
      public CUIButton Button { get; }
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

      public Action OnClick { set { Button.OnClick = value; } }
      public event Action? Click
      {
        add => Button.Click += value;
        remove => Button.Click -= value;
      }

      public ButtonField() : base()
      {
        this["button"] = Button = new CUIButton("Click");
        this["label"] = Label = new SolidTextBlock("do something funny") { Flex = 1 };
      }
    }
  }
}