using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class ResizeHandle : CUIVisualComponent, IModule, IAware
  {
    public static Vector2 DefaultSize = new Vector2(22, 22);


    public object HostComponent { get; set; }
    public string HostPropName { get; set; }

    private IResizable host;
    [In]
    public IResizable Host
    {
      get => host;
      set
      {
        if (host is not null) DisconnectFromHost(host);
        host = value;
        if (host is not null) ConnectToHost(host);
      }
    }

    public override Layout? Layout { get; protected set; } = new CUIDummyLayout();


    public SimpleTexture Background { get; } = new();

    public override bool Visible
    {
      get => Background.Visible;
      set => Background.Visible = value;
    }

    public Vector2 Anchor
    {
      get => SelfAnchor;
      set
      {
        SelfAnchor = value;
        ParentAnchor = value;
        StaticPointAnchor = Vector2.One - value;
      }
    }
    public Vector2 ParentAnchor { get; set; }
    public Vector2 StaticPointAnchor { get; set; }
    public Vector2 SelfAnchor { get; set; } = new Vector2(1, 1);


    public Vector2 Size { get; set; } = DefaultSize;

    public bool Displayed { get; set; }
    public bool Grabbed { get; private set; }

    public Vector2 GrabPoint { get; private set; }
    public Vector2 GrabOffset { get; private set; }
    public Vector2 StartSelfAnchorPoint { get; private set; }


    public override CUIRect OuterRect { get => Rect; set => Rect = value; }
    public override CUIRect ChildrenRect { get => Rect; set => Rect = value; }
    public CUIRect Rect
    {
      get => Background.Rect;
      set => Background.Rect = value;
    }

    private void ConnectToHost(IResizable host)
    {
      UpdateRect();
    }

    private void DisconnectFromHost(IResizable host)
    {
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
        SelfAnchorPoint, ParentAnchor
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

    public ResizeHandle()
    {
      Background.Sprite = CUIDefaultSprite.Angle;

      Anchor = new Vector2(1, 1);
      Background.Sprite.Effects = SpriteEffects.FlipHorizontally;
      Background.Color = Color.White;

      Background.MouseDown.Add(Grab);

      Background.ConsumeMouseEvents = true;
    }
  }
}