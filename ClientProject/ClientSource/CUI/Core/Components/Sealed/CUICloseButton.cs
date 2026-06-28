using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComponentGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{
  //TODO this should really be inheried from some CUIIconButton
  public partial class CUICloseButton : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUICloseButton>((c) =>
    {
      c.Background.Color = c.Palette.Colors["border"];
      c.ForeColor = c.Palette.Colors["outercontrols"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Icon.Sprite = CUIDefaultSprite.Cross;
      Absolute = new CUINullRect(DefaultSize);
      ConsumeMouseClicks = true;
    }

    public static Vector2 DefaultSize => ResizeHandle.DefaultSize;

    public SimpleTexture Icon { get; } = new();

    private Color _ForeColor; public Color ForeColor
    {
      get => _ForeColor;
      set
      {
        _ForeColor = value;
        DetermineColor();
      }
    }

    public void DetermineColor()
    {
      if (MousePressed) Icon.Color = ForeColor;
      else if (MouseOver) Icon.Color = ForeColor.Multiply(0.9f);
      else Icon.Color = ForeColor.Multiply(0.7f);
    }

    protected override void UpdateRects()
    {
      base.UpdateRects();
      Icon.Rect = Rect;
    }

    [CUISerializable]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        Icon.Visible = value;
      }
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(DefaultSize);

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return Icon.VisualWrapper;
    }

    public CUICloseButton() : base()
    {
      MouseDown += (c, e) => Commands.SendUp("close");

      MouseOff += (c, e) => DetermineColor();
      MouseOn += (c, e) => DetermineColor();
      DetermineColor();
    }
  }
}