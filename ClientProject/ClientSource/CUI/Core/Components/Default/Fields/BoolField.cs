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
    public class BoolField : HorizontalBackPanel
    {
      public CUICheckBox Checkbox { get; }
      public CUITextBlock Label { get; }

      public bool State
      {
        get => Checkbox.State;
        set => Checkbox.State = value;
      }

      public Action<bool> OnToggle { set => Checkbox.OnToggle = value; }

      public string Text
      {
        get => Label.Text;
        set => Label.Text = value;
      }

      public BoolField() : base()
      {
        this["checkbox"] = Checkbox = new CUICheckBox();
        this["label"] = Label = new CUITextBlock() { Flex = 1 };
      }

      public BoolField(string label) : this()
      {
        Text = label;
      }
    }
  }
}