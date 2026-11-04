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
    public class ButtonGroupField : HorizontalBackPanel
    {
      public ButtonGroup ButtonGroup { get; }
      public CUITextBlock Label { get; }

      public string Text
      {
        get => Label.Text;
        set => Label.Text = value;
      }

      public Action<string> OnSelected { set { ButtonGroup.OnSelected = value; } }
      public event Action<string>? Selected
      {
        add => ButtonGroup.Selected += value;
        remove => ButtonGroup.Selected -= value;
      }

      public string? SelectedOption { get => ButtonGroup.SelectedOption; set => ButtonGroup.SelectedOption = value; }
      public List<string> Options { set => ButtonGroup.Options = value; }

      public CUIRadioButton.RadioGroup Group
      {
        get => ButtonGroup.Group;
        set => ButtonGroup.Group = value;
      }
      public string GroupName
      {
        get => ButtonGroup.GroupName;
        set => ButtonGroup.GroupName = value;
      }

      public ButtonGroupField() : base()
      {
        this["label"] = Label = new SolidTextBlock();
        this["buttons"] = ButtonGroup = new ButtonGroup() { Flex = 1 };
      }
    }
  }
}