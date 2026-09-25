using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public partial class ResizeHandle : CUIVisualComponent
  {
    /// <summary>
    /// Used everywhere  
    /// why 22? it's default height of barotrauma text
    /// </summary>
    //TODO akshually should move all such sizes to some global class
    public static Vector2 DefaultSize = new Vector2(22, 22);
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<ResizeHandle>((c) =>
    {
      c.Background.Color = c.Palette["main"] * 0.5f;
    });

    public new Action<ResizeHandle> Style
    {
      set => PersonalStyle = new CUIActionStyle<ResizeHandle>("personal", value);
    }

    protected override void InitStyle()
    {
      base.InitStyle();

      Absolute = new CUINullRect(w: DefaultSize.X, h: DefaultSize.Y);
    }

    public static ResizeHandle CreateAngle(float x, float y)
    {
      ResizeHandle handle = new ResizeHandle(x, y)
      {
        Background = { Sprite = CUISprite.Angle },
      };

      if (x > 0.5f)
      {
        handle.Background.Effects = handle.Background.Effects | SpriteEffects.FlipHorizontally;
      }

      if (y > 0.5f)
      {
        handle.Background.Effects = handle.Background.Effects | SpriteEffects.FlipVertically;
      }

      return handle;
    }


    private IResizable host; public IResizable Host
    {
      get => host;
      set
      {
        host = value;
        UpdateRect();
      }
    }

    public override Layout? Layout { get; protected set; } = new CUIDummyLayout();

    public ResizeHandle()
    {
      Layout = new CUIDummyLayout();

      InheritPalette = true;

      Background.MouseDown.Add(Grab);
      Background.ConsumeMouseEvents = true;


      Events.Route(Background);
    }

    public ResizeHandle(float x, float y) : this(new Vector2(x, y)) { }
    public ResizeHandle(Vector2 anchor) : this()
    {
      Anchor = anchor;
      ParentAnchor = anchor;
      StaticPointAnchor = Vector2.One - anchor;
    }
  }
}