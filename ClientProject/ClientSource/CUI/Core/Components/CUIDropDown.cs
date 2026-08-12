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
  public partial class CUIDropDown : CUIComponent, IComponent
  {
    public string Selected
    {
      get => SelectedBtn.Text;
      set => SelectedBtn.Text = value;
    }

    public IEnumerable<string> Options
    {
      set
      {
        OptionBox.Children.Clear();
        foreach (string option in value)
        {
          OptionBox.Children.Add(new CUIButton(option)
          {
            OnMouseDown = (c, e) => Select(option),
          });
        }
      }
    }

    private void Select(string option)
    {
      Selected = option;
    }


    public bool Open { get; set; }

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

    private void HandleGlobalMouseClick(CUIMouseDownEvent e) => Open = false;

    private CUIButton SelectedBtn { get; }
    private CUIComponent OptionBox { get; }

    // private CUINullVector2 MinSize;
    // protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
    //   SelectedBtn.TextBlock.ForcedSize.X,
    //   SelectedBtn.TextBlock.ForcedSize.Y
    // );

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return SelectedBtn.VisualWrapper;

      if (!Open)
      {
        yield return OptionBox.VisualWrapper;
      }


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

      this["selected"] = SelectedBtn = new CUIButton("Unset");
      SelectedBtn.MouseDown += (c, e) => Open = !Open;
    }
  }
}