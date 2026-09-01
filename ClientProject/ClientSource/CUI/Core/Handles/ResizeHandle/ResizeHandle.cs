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
  public class ResizeHandle : CUIVisualComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<ResizeHandle>((c) =>
    {
      c.Background.Color = c.Palette["main"] * 0.5f;
    });

    public static Vector2 DefaultSize = new Vector2(22, 22);

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


    public SimpleTexture Background { get; } = new();

    public override bool Visible
    {
      get => Background.Visible;
      set => Background.Visible = value;
    }

    public new Vector2 Anchor
    {
      get => SelfAnchor;
      set
      {
        base.Anchor = value;
        SelfAnchor = value;
        ParentAnchor = value;
        StaticPointAnchor = Vector2.One - value;
      }
    }



    public Vector2 StaticPointAnchor { get; set; }
    public Vector2 SelfAnchor { get; set; } = new Vector2(1, 1);


    public Vector2 Size => Absolute.Size;

    public bool Grabbed { get; private set; }

    public Vector2 GrabPoint { get; private set; }
    public Vector2 GrabOffset { get; private set; }
    public Vector2 StartSelfAnchorPoint { get; private set; }


    public override bool MouseOver => Background.MouseOver;
    public override bool MousePressed => Background.MousePressed;

    public override CUIRect OuterRect { get => Rect; set => Rect = value; }
    public override CUIRect ChildrenRect { get => Rect; set => Rect = value; }
    public CUIRect Rect
    {
      get => Background.Rect;
      set => Background.Rect = value;
    }


    //BRUH Why is this inverted, why not just set Rect from Host.UpdateRect?
    public void UpdateRect()
    {
      if (Host is null)
      {
        Rect = new CUIRect(Vector2.Zero, Size);
      }
      else
      {
        Rect = new CUIRect(
          CUIAnchor.ChildPosIn(Host.Rect, Anchor, Size),
          Size
        );
      }
    }


    private void Grab(CUIMouseEvent e)
    {
      if (!Host.TryGrab(this)) return;

      Grabbed = true;
      GrabOffset = Rect.LeftTop - e.Pos;
      GrabPoint = e.Pos;
      StartSelfAnchorPoint = CUIAnchor.PosFromAnchor(Rect, SelfAnchor);
      host.HubMouseMoved += Update;
      host.HubMouseUp += Release;
    }

    public void Update(CUIMouseEvent e)
    {
      Vector2 delta = e.Pos - GrabPoint;
      Vector2 SelfAnchorPoint = StartSelfAnchorPoint + delta;
      Resize(SelfAnchorPoint);
    }

    private void Resize(Vector2 SelfAnchorPoint)
    {
      Vector2 staticPoint = CUIAnchor.PosFromAnchor(Host.Rect, StaticPointAnchor);

      CUIRect rect = CUIAnchor.RectFrom2PointsWithAnchors(
        staticPoint, StaticPointAnchor,
        SelfAnchorPoint, ParentAnchor.Value
      );

      Vector2 size = new Vector2(
        Math.Max(rect.Width, Host.MinSize.X),
        Math.Max(rect.Height, Host.MinSize.Y)
      );

      Host.ResizeToAbsoluteRect(
        CUIAnchor.RectFromPointAndSize(
          staticPoint, StaticPointAnchor, size
        )
      );
    }

    private void Release(CUIMouseEvent e)
    {
      Vector2 delta = e.Pos - GrabPoint;
      Vector2 SelfAnchorPoint = StartSelfAnchorPoint + delta;

      Resize(SelfAnchorPoint);


      Grabbed = false;
      host.HubMouseMoved -= Update;
      host.HubMouseUp -= Release;
      host.Release(this);
    }

    public void ForceRelease()
    {
      Grabbed = false;
      host.HubMouseMoved -= Update;
      host.HubMouseUp -= Release;
      host.Release(this);
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (Displayed) yield return Background.VisualWrapper;
    }

    public ResizeHandle(float x, float y) : this(new Vector2(x, y)) { }
    public ResizeHandle(Vector2 anchor)
    {
      Layout = new CUIDummyLayout();
      Layout.ConnectTo(new Adapters_Part.CUIPlainLayout_Host_Part() { Self = this });

      Absolute = new CUINullRect(w: DefaultSize.X, h: DefaultSize.Y);
      InheritPalette = true;

      Anchor = anchor;

      Background.Sprite = CUISprite.Angle;
      Background.Color = Color.White;

      if (anchor.X > 0.5f)
      {
        Background.Effects = Background.Effects | SpriteEffects.FlipHorizontally;
      }

      if (anchor.Y > 0.5f)
      {
        Background.Effects = Background.Effects | SpriteEffects.FlipVertically;
      }

      Background.MouseDown.Add(Grab);
      Background.ConsumeMouseEvents = true;
    }
  }
}