using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIComponentTreeSelect : CUIVerticalList
  {
    public CUIRadioButton.RadioGroup RadioGroup { get; private set; }


    private CUIVisualComponent _Root; public CUIVisualComponent Root
    {
      get => _Root;
      set
      {
        _Root = value;
        RadioGroup = CUIRadioButton.RadioGroup.GetOrCreate($"CUIComponentTreeSelect|{Root}");
        Refresh();
      }
    }

    public event Action<CUIVisualComponent> Selected;

    public void Select(string relatievAKA)
    {
      SelectedComponent = Root.Get(relatievAKA);
    }

    public CUIVisualComponent SelectedComponent
    {
      get => RadioGroup.Current?.GetData<CUIVisualComponent>("component");
      set
      {
        RadioGroup.ClearSelection();

        foreach (CUIRadioButton btn in ComponentTree.Children.As<CUIRadioButton>())
        {
          if (btn.GetData<CUIVisualComponent>("component") == value)
          {
            btn.Select();
          }
        }
      }
    }

    public CUIVerticalList ComponentTree { get; }

    public float Offset { get; set; } = 10.0f;

    public override void Refresh()
    {
      ComponentTree.Clear();
      if (Root is null) return;

      List<CUIComponent> toAdd = new();

      void AddChildrenRec(CUIVisualComponent parent, int depth)
      {
        toAdd.Add(new CUIRadioButton(parent.ToString())
        {
          TextAnchor = CUIAnchor.LeftCenter,
          Margin = new CUISizes(left: depth * Offset),
          Background = { Sprite = CUISprite.DimmedVerticalLight },
          Group = RadioGroup,
          Data = new() { ["component"] = parent },
          Palette = CUICore.Palettes.Tertiary,
        });

        foreach (CUIVisualComponent child in parent.Children)
        {
          AddChildrenRec(child, depth + 1);
        }
      }

      AddChildrenRec(Root, 0);

      RadioGroup.Selected += (CUIRadioButton) =>
      {
        Selected?.Invoke(CUIRadioButton?.GetData<CUIVisualComponent>("component"));
      };

      foreach (CUIComponent child in toAdd)
      {
        ComponentTree.Add(child);
      }
    }

    public CUIComponentTreeSelect() : base()
    {
      this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
      this["layout"]["header"] = new CUIDefault.HorizontalPanel();
      this["layout"]["header"]["caption"] = new CUITextBlock("Component Tree") { Flex = 1 };

      this["layout"]["main"] = ComponentTree = new CUIDefault.VerticalBackPanel()
      {
        Flex = 1,
        Scrollable = true,
      };

      DeepPalette = CUICore.Palettes.Secondary;
    }
  }
}