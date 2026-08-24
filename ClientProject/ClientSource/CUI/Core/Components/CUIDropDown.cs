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
using CUILibs;

namespace CrabUI
{
  public partial class CUIDropDown : CUIComponent, IComponent
  {
    public string Selected
    {
      get => SelectedBtn.Text;
      set
      {
        SelectedBtn.Text = value;
        Select?.Invoke(value);
      }
    }

    public Action<string> OnSelect { set { Select += value; } }
    public event Action<string> Select;

    public IEnumerable<string> Options
    {
      get => OptionBox.Children.Select(c => (c as CUIButton).Text);
      set
      {
        OptionBox.Children.Clear();
        foreach (string option in value)
        {
          Add(option);
        }
      }
    }

    public void Add(string option)
    {
      OptionBox.Children.Add(new CUIButton(option)
      {
        Background = { Sprite = CUISprite.White },
        Borders = {
          Sizes = new CUISizes(bottom:1),
          Color = Color.White,
        },
        OnMouseDown = (e) =>
        {
          Selected = option;
          IsOpen = false;
        },
        Style = (c) =>
        {
          c.MasterColor = c.Palette["main"].To(Color.Black, 0.3f);
          c.Borders.Color = c.Palette["border"];
        },
      });
    }

    public void Remove(string option)
    {
      CUIButton? btn = (CUIButton)OptionBox.Children.FirstOrDefault(
        c => (c as CUIButton).Text == option
      );

      if (btn != null)
      {
        OptionBox.Children.Remove(btn);
      }
    }

    public bool Has(string option)
    {
      foreach (CUIVisualComponent child in OptionBox.Children)
      {
        if (child is CUIButton btn)
        {
          if (btn.Text == option) return true;
        }
      }

      return false;
    }

    public void Clear()
    {
      OptionBox.Children.Clear();
    }

    private bool _IsOpen; public bool IsOpen
    {
      get => _IsOpen;
      set
      {
        _IsOpen = value;
        VisualRestructureNotifier.Notify();
      }
    }

    protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      base.OnAttachedToMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseDown.Add(HandleGlobalMouseClick);
    }
    protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      base.OnDetachedFromMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseDown.Remove(HandleGlobalMouseClick);
    }

    private bool WasClosedFromGlobalMouseDown; //HACK
    private void HandleGlobalMouseClick(CUIMouseDownEvent e)
    {
      WasClosedFromGlobalMouseDown = false;
      if (IsOpen)
      {
        IsOpen = false;
        WasClosedFromGlobalMouseDown = true;
      }
    }

    private CUIButton SelectedBtn { get; }
    private CUIComponent OptionBox { get; }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return SelectedBtn.VisualWrapper;

      if (IsOpen) yield return OptionBox.VisualWrapper;

      yield return Borders.VisualWrapper;
    }

    public CUIDropDown()
    {
      FitContent = new CUIBool2(true, true);

      this["pin"] = new CUIComponent()
      {
        Anchor = CUIAnchor.CenterTop,
        ParentAnchor = CUIAnchor.CenterBottom,
        FitContent = new CUIBool2(true, false),
      };

      this["pin"]["optionbox"] = OptionBox = new CUIVerticalList()
      {
        FitContent = new(true, true),
        Anchor = CUIAnchor.CenterTop,
      };

      this["selected"] = SelectedBtn = new CUIButton("Unset")
      {
        Relative = new CUINullRect(w: 1),
        TextAnchor = CUIAnchor.LeftCenter,
        OnMouseDown = (e) =>
        {
          if (!IsOpen && WasClosedFromGlobalMouseDown) return;

          IsOpen = !IsOpen;
        },
      };
    }
  }
}