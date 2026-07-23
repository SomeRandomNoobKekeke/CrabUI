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
  public static partial class CUIDefault
  {
    public abstract class InputField : CUIHorizontalList
    {
      public string CommandName { get; set; } = "setvalue";

      public string Key
      {
        get => Label.Text;
        set => Label.Text = value;
      }

      public string RawValue
      {
        get => Input.Text;
        set => Input.Text = value;
      }

      public CUITextBlock Label { get; }
      public CUITextInput Input { get; }

      public Func<string, bool> Validation
      {
        get => Input.ValidationFunc;
        set => Input.ValidationFunc = value;
      }

      public InputField() : base()
      {
        FitContent = new CUIBool2(false, true);

        this["label"] = Label = new CUITextBlock()
        {
          TextAnchor = CUIAnchor.LeftCenter,
          Padding = new CUISizes(2, 10, 2, 2),
        };
        this["input"] = Input = new CUITextInput()
        {
          Flex = 1,
          OnValidInput = (value) => this["input"].Commands.SendUp(CommandName, new string[]
          {
            Key, value,
          })
        };
      }
    }
  }
}