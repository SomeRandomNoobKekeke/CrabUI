using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CursedUI
{
  public partial class CUIRenamableButton : CUIButton
  {
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<CUIRenamableButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
      c.TextColor = c.Palette["text"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Background.Sprite = CUISprite.Vignette;
    }

    private string TextBeforeRenaming;
    public void StartRenaming() => IsRenaming = true;
    private bool _IsRenaming; public bool IsRenaming
    {
      get => _IsRenaming;
      set
      {
        RenameOverlay.Displayed = value;

        if (!_IsRenaming && value)
        {
          TextBeforeRenaming = Text;
          RenameOverlay.Text = Text;
          RenameOverlay.Focus();
        }

        if (_IsRenaming && !value)
        {
          Text = RenameOverlay.Text;
          Renamed?.Invoke(TextBeforeRenaming, RenameOverlay.Text);
          RenameOverlay.Blur();
        }

        _IsRenaming = value;
      }
    }

    public Action<string, string> OnRenamed { set { Renamed += value; } }
    public event Action<string, string> Renamed;

    protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      base.OnAttachedToMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseDown.Add(HandleGlobalClick);
    }
    protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      base.OnDetachedFromMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseDown.Remove(HandleGlobalClick);
    }

    private void HandleGlobalClick(CUIMouseDownEvent e)
    {
      if (Background.Contains(e.Pos)) return;
      IsRenaming = false;
    }

    public CUITextInput RenameOverlay { get; }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;
      yield return TextState.TextBlock.VisualWrapper;
      yield return RenameOverlay.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Border.VisualWrapper;
    }

    public CUIRenamableButton() : base()
    {
      TextAnchor = CUIAnchor.LeftCenter;

      this["overlay"] = RenameOverlay = new CUITextInput()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        FocusedSprite = CUISprite.GlowingEdges,
        OnKeyPressed = (e) =>
        {
          if (e.Key == Keys.Enter) IsRenaming = false;
        },
        Palette = CUICore.Palettes.Primary,
        OnInput = (s) => Text = RenameOverlay.Text,
      };

      IsRenaming = false;
    }
    public CUIRenamableButton(string text) : this()
    {
      Text = text;
    }

  }
}