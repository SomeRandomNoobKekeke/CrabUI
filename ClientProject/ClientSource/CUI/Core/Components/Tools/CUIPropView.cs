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
  //Test LINK:\ClientProject\InMemory\CUITest\Snapshots\Tests\Components\CUIPropView.cs
  public class CUIPropView : CUIPage
  {

    private CUIVisualComponent _Target; public CUIVisualComponent Target
    {
      get => _Target;
      set
      {
        _Target = value;
        Refresh();
      }
    }

    public override void Refresh()
    {
      this["layout"]["Props"].Children.Clear();

      if (Target is null) return;

      foreach (var (key, value) in Target.As_StringDictionary)
      {
        this["layout"]["Props"].Children.Add(new CUITextBlock($"{key} - {value}")
        {
          TextAnchor = CUIAnchor.LeftCenter,
        });
      }
    }

    public double AutoUpdateInterval { get; set; } = 0.25;

    private double lastUpdateTime;
    private void TryAutoUpdate(double totalTime)
    {
      if (totalTime - lastUpdateTime > AutoUpdateInterval)
      {
        lastUpdateTime = totalTime;
        Refresh();
      }
    }

    public bool IsAutoUpdating { get; private set; }
    public void SetAutoUpdate(bool state)
    {
      if (!IsAutoUpdating && state) CUICore.OnUpdate += TryAutoUpdate;
      if (IsAutoUpdating && !state) CUICore.OnUpdate -= TryAutoUpdate;
      IsAutoUpdating = state;
    }

    public CUIPropView() : base()
    {
      this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
      this["layout"]["header"] = new CUIDefault.HorizontalPanel();
      this["layout"]["header"]["Update"] = new CUIButton("Update")
      {
        Flex = 1,
        OnMouseDown = (e) => Refresh(),
        Palette = CUICore.Palettes.Secondary
      };
      this["layout"]["header"]["autoupdate"] = new CUIToggleButton("AutoUpdate")
      {
        OnToggle = (state) => SetAutoUpdate(state),
        Palette = CUICore.Palettes.Tertiary
      };


      this["layout"]["Props"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
      };
    }
  }
}