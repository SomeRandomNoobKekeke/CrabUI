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
    public class TextInputField : HorizontalBackPanel
    {
      public CUITextInput TextInput { get; }
      public CUITextBlock Label { get; }

      public Action<string> OnInput { set => TextInput.OnInput = value; }
      public event Action<string>? Input
      {
        add => TextInput.Input += value;
        remove => TextInput.Input -= value;
      }


      public string Text
      {
        get => TextInput.Text;
        set => TextInput.Text = value;
      }

      public string LabelText
      {
        get => Label.Text;
        set => Label.Text = value;
      }

      public TextInputField() : base()
      {
        this["label"] = Label = new SolidTextBlock();
        this["input"] = TextInput = new CUITextInput() { Flex = 1 };
      }

      public TextInputField(string text) : this()
      {
        LabelText = text;
      }
    }
  }
}