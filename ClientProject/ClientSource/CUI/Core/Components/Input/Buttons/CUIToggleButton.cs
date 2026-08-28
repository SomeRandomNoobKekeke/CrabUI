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
  [GeneratedComponent]
  public partial class CUIToggleButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIToggleButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(2, 4, 2, 4);
      Background.Sprite = CUISprite.Vignette;
    }

    public TextToggleState_Part OnState { get; } = new();
    public TextToggleState_Part OffState { get; } = new();

    private TextToggleState_Part SelectedTextState;

    [CUISerializableProp]
    public Color OnColor
    {
      get => OnState.BackgroundColor;
      set => OnState.BackgroundColor = value;
    }

    [CUISerializableProp]
    public Color OffColorHovered
    {
      get => OffState.BackgroundColorHovered;
      set => OffState.BackgroundColorHovered = value;
    }

    [CUISerializableProp]
    public Color OffColor
    {
      get => OffState.BackgroundColor;
      set => OffState.BackgroundColor = value;
    }


    private bool _State; public bool State
    {
      get => _State;
      set
      {
        _State = value;
        SelectedTextState = value ? OnState : OffState;
        DetermineColor();
        VisualRestructureNotifier.Notify();
      }
    }

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

    public Action<bool> OnToggle { set { Toggle += value; } }
    public event Action<bool> Toggle;

    public new Action<CUIButton> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIButton>("personal", value);
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      SelectedTextState.TextBlock.ForcedSize.X + Padding.FullWidth,
      SelectedTextState.TextBlock.ForcedSize.Y + Padding.FullHeigth
    );

    protected override void UpdateRects()
    {
      base.UpdateRects();
      OnState.TextBlock.Rect = ChildrenRect;
      OffState.TextBlock.Rect = ChildrenRect;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        SelectedTextState.TextBlock.Visible = value;
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;
      yield return SelectedTextState.TextBlock.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Borders.VisualWrapper;
    }


    #region Forwarded to TextState
    public string Text
    {
      get => OnState.Text;
      set
      {
        OnState.Text = value;
        OffState.Text = value;
      }
    }
    public Color TextColor
    {
      get => OnState.TextColor;
      set
      {
        OnState.TextColor = value;
        OffState.TextColor = value;
      }
    }
    public float Scale
    {
      get => OnState.Scale;
      set
      {
        OnState.Scale = value;
        OffState.Scale = value;
      }
    }
    public ResizeStrategy ResizeStrategy
    {
      get => OnState.ResizeStrategy;
      set
      {
        OnState.ResizeStrategy = value;
        OffState.ResizeStrategy = value;
      }
    }
    public Vector2 TextAnchor
    {
      get => OnState.TextAnchor;
      set
      {
        OnState.TextAnchor = value;
        OffState.TextAnchor = value;
      }
    }
    public SpriteEffects SpriteEffects
    {
      get => OnState.SpriteEffects;
      set
      {
        OnState.SpriteEffects = value;
        OffState.SpriteEffects = value;
      }
    }
    public float LayerDepth
    {
      get => OnState.LayerDepth;
      set
      {
        OnState.LayerDepth = value;
        OffState.LayerDepth = value;
      }
    }
    public CUIFont Font
    {
      get => OnState.Font;
      set
      {
        OnState.Font = value;
        OffState.Font = value;
      }
    }

    public string RealText => OnState.RealText;
    public Vector2 RawTextSize => OnState.RawTextSize;
    public Vector2 TextDrawPosition => OnState.TextDrawPosition;
    public CUINullVector2 ForcedSize => OnState.ForcedSize;
    public float RealScale => OnState.RealScale;
    #endregion

    public CUIToggleButton() : base()
    {
      MouseDown += (e) =>
      {
        State = !State;
        if (PlaySound) SoundPlayer.PlayUISound(ClickSound);
        Toggle?.Invoke(State);
        if (Emit != null) Commands.SendUp(Emit, Text);
      };

      MouseOff += (e) => DetermineColor();
      MouseOn += (e) => DetermineColor();
      State = false;

      ConsumeMouseEvents = true;
    }
    public CUIToggleButton(string text) : this()
    {
      Text = text;
    }
  }
}