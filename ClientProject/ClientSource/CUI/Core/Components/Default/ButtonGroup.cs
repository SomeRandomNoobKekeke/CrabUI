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
    public class ButtonGroup : HorizontalBackPanel
    {
      public Action<string> OnSelected { set { Selected += value; } }
      public event Action<string>? Selected;

      private string? _SelectedOption; public string? SelectedOption
      {
        get => Group?.Current?.Text;
        set
        {
          _SelectedOption = value;
          Group.Select(this.Get<CUIRadioButton>(value));
        }
      }

      public List<string> Options
      {
        set
        {
          Clear();
          foreach (string option in value)
          {
            this[option] = new CUIRadioButton(option)
            {
              Flex = 1,
              Group = Group,
            };
          }

          if (_SelectedOption != null)
          {
            Group.Select(this.Get<CUIRadioButton>(_SelectedOption));
          }
        }
      }

      private void HandleSelect(CUIRadioButton btn) => Selected?.Invoke(btn.Text);

      private CUIRadioButton.RadioGroup _Group; public CUIRadioButton.RadioGroup Group
      {
        get => _Group;
        set
        {
          if (_Group != null) _Group.Selected -= HandleSelect;
          _Group = value;
          if (_Group != null) _Group.Selected += HandleSelect;

          foreach (CUIRadioButton child in Children)
          {
            child.Group = value;
          }
        }
      }
      public string GroupName
      {
        get => Group?.Name;
        set => Group = CUIRadioButton.RadioGroup.GetOrCreate(value);
      }

    }
  }
}