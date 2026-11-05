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
    public class IconButtonField : HorizontalBackPanel
    {
      public CUIIconButton Button { get; }
      public CUITextBlock Label { get; }

      public Action OnClick { set { Button.OnClick = value; } }
      public event Action Click
      {
        add => Button.Click += value;
        remove => Button.Click -= value;
      }

      public string Text
      {
        get => Label.Text;
        set => Label.Text = value;
      }

      public IconButtonField() : base()
      {
        this["button"] = Button = new CUIIconButton();
        this["label"] = Label = new CUITextBlock() { Flex = 1 };
      }

      public IconButtonField(CUISprite icon, string text) : this()
      {
        Button.Icon = icon;
        Text = text;
      }
    }
  }
}