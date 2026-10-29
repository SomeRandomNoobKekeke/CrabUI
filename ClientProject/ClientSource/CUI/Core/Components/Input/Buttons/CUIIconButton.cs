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
  //LINK:\ClientProject\Tests\CUITest\Snapshots\Tests\Components\CUIIconButton.cs
  public partial class CUIIconButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<CUIIconButton>((c) =>
    {
      c.Background.Color = c.Palette["main"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      // Padding = new(2, 2, 2, 2);
      Background.Sprite = CUISprite.DimmedVertical;
      MasterColor = Color.White;
    }

    public new Action<CUIIconButton> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIIconButton>("personal", value);
    }

    private IconBlock IconBlock = new();

    [CUISerializableProp]
    public CUISprite Icon
    {
      get => IconBlock.Icon;
      set
      {
        IconBlock.Icon = value;
        LayoutMarker.Mark(LayoutMarker.Pattern.AbsoluteProp);
      }
    }

    [CUISerializableProp]
    public float Scale
    {
      get => IconBlock.Scale;
      set => IconBlock.Scale = value;
    }

    [CUISerializableProp]
    public Vector2 IconAnchor
    {
      get => IconBlock.Anchor;
      set => IconBlock.Anchor = value;
    }


    [CUISerializableProp]
    public Color MouseOverColor { get; set; }
    [CUISerializableProp]
    public Color MousePressedColor { get; set; }
    [CUISerializableProp]
    public Color InactiveColor { get; set; }


    public override Color MasterColor
    {
      set
      {
        InactiveColor = value.MultOpaque(0.8f);
        MouseOverColor = value.MultOpaque(0.9f);
        MousePressedColor = value;
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      Background.Color = Disabled ? Palette["disabled"] : Palette["main"];

      IconBlock.Color = InactiveColor;
      if (MouseOver) IconBlock.Color = MouseOverColor;
      if (MousePressed) IconBlock.Color = MousePressedColor;
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      IconBlock.ForcedSize.X + Padding.FullWidth,
      IconBlock.ForcedSize.Y + Padding.FullHeigth
    );

    protected override void UpdateRects()
    {
      base.UpdateRects();
      IconBlock.Rect = ChildrenRect;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        IconBlock.Visible = value;
      }
    }

    public override float Transparency
    {
      get => base.Transparency;
      set
      {
        base.Transparency = value;
        IconBlock.Transparency = value;
      }
    }

    public Action OnClick { set { Click += value; } }
    /// <summary>
    /// This event happens when click passes, e.g. MouseDown will trigger on Disabled buttons but Click won't
    /// </summary>
    public event Action Click;

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;
      yield return IconBlock.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Border.VisualWrapper;
    }

    public CUIIconButton() : base()
    {
      MouseOff += (e) => DetermineColor();
      MouseOn += (e) => DetermineColor();
      DetermineColor();

      MouseDown += (e) =>
      {
        if (Disabled) return;
        if (!e.Mouse.M1.Down) return;
        if (PlaySound) SoundPlayer.PlayUISound(ClickSound);
        if (Emit != null) Commands.SendUp(Emit);
        Click?.Invoke();
      };

      ConsumeMouseEvents = true;
    }
    public CUIIconButton(CUISprite icon) : this()
    {
      Icon = icon;
    }
  }
}