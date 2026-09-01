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
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIRenamableButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
      c.TextColor = c.Palette["text"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(2, 0, 2, 0);
      Background.Sprite = CUISprite.Vignette;
    }

    public void StartRenaming() => IsRenaming = true;
    private bool _IsRenaming; public bool IsRenaming
    {
      get => _IsRenaming;
      set
      {
        TextInput.Displayed = value;

        if (!_IsRenaming && value)
        {
          TextInput.Text = Text;
          TextInput.Focus();
        }

        if (_IsRenaming && !value)
        {
          Text = TextInput.Text;
          Renamed?.Invoke(TextInput.Text);
          TextInput.Blur();
        }

        _IsRenaming = value;
      }
    }

    public Action<string> OnRenamed { set { Renamed += value; } }
    public event Action<string> Renamed;

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

    public CUITextInput TextInput { get; }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;
      yield return TextState.TextBlock.VisualWrapper;
      yield return TextInput.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Borders.VisualWrapper;
    }

    public CUISizes TextPadding { get; set; } = new CUISizes(2, 4, 2, 4);
    protected override void UpdateRects()
    {
      base.UpdateRects();
      TextState.TextBlock.Rect = ChildrenRect - TextPadding;//HACK idk how to apply padding only to TextBlock
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      TextState.TextBlock.ForcedSize.X + TextPadding.FullWidth,
      TextState.TextBlock.ForcedSize.Y + TextPadding.FullHeigth
    );

    public CUIRenamableButton() : base()
    {
      TextAnchor = CUIAnchor.LeftCenter;

      this["input"] = TextInput = new CUITextInput()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        FocusedSprite = CUISprite.GlowingEdges,
        OnKeyPressed = (e) =>
        {
          if (e.Key == Keys.Enter) IsRenaming = false;
        },
        Palette = CUICore.Palettes.Primary,
        OnInput = (s) => Text = TextInput.Text,
      };

      IsRenaming = false;
    }
    public CUIRenamableButton(string text) : this()
    {
      Text = text;
    }

  }
}