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
  public partial class CUIToggleIconButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIToggleIconButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      // Padding = new(2, 2, 2, 2);
      Background.Sprite = CUISprite.Vignette;
    }

    public IconBlock OnIconBlock { get; } = new();
    public IconBlock OffIconBlock { get; } = new();

    private IconBlock SelectedIconBlock;

    public CUISprite Icon
    {
      get => OnIconBlock.Icon;
      set => OnIconBlock.Icon = value;
    }

    public float Scale
    {
      get => OnIconBlock.Scale;
      set => OnIconBlock.Scale = value;
    }

    public Vector2 IconAnchor
    {
      get => OnIconBlock.Anchor;
      set => OnIconBlock.Anchor = value;
    }

    [CUISerializableProp]
    public Color OnColor { get; set; }

    [CUISerializableProp]
    public Color OffColorHovered { get; set; }

    [CUISerializableProp]
    public Color OffColor { get; set; }


    public override Color MasterColor
    {
      set
      {
        OnColor = value.MultOpaque(0.7f);
        OffColorHovered = value.MultOpaque(0.5f);
        OffColor = value.MultOpaque(0.4f);
        DetermineColor();
      }
    }

    private bool _State; public bool State
    {
      get => _State;
      set
      {
        _State = value;
        SelectedIconBlock = value ? OnIconBlock : OffIconBlock;

        DetermineColor();
        LayoutMarker.Mark(LayoutMarker.Pattern.FromParentAndDown);
      }
    }

    public Action<bool> OnToggle { set { Toggle += value; } }
    public event Action<bool> Toggle;

    public override void DetermineColor()
    {
      if (State)
      {
        Background.Color = OnColor;
      }
      else
      {
        Background.Color = OffColor;
        if (MouseOver) Background.Color = OffColorHovered;
      }
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      SelectedIconBlock.Icon.Size.X + Padding.FullWidth,
      SelectedIconBlock.Icon.Size.Y + Padding.FullHeigth
    );

    protected override void UpdateRects()
    {
      base.UpdateRects();
      OnIconBlock.Rect = ChildrenRect;
      OffIconBlock.Rect = ChildrenRect;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        OnIconBlock.Visible = value;
        OffIconBlock.Visible = value;
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;
      yield return SelectedIconBlock.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Borders.VisualWrapper;
    }

    public CUIToggleIconButton() : base()
    {
      MouseDown += (e) =>
      {
        State = !State;
        if (PlaySound) SoundPlayer.PlayUISound(ClickSound);
        Toggle?.Invoke(State);
        if (Emit != null) Commands.SendUp(Emit);
      };

      MouseOff += (e) => DetermineColor();
      MouseOn += (e) => DetermineColor();


      OffIconBlock.Icon = CUISprite.EmptyIcon;
      State = false;

      ConsumeMouseEvents = true;
    }
    public CUIToggleIconButton(CUISprite icon) : this()
    {
      Icon = icon;
    }
  }
}