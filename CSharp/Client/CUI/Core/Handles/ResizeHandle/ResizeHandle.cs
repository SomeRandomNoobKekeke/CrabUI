using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public class ResizeHandle : CUIVisualComponent, IModule, IAware
  {
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

    public SimpleTexture Background { get; } = new();

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


    public Vector2 Size { get; set; } = new Vector2(15, 10);

    public bool Visible { get; set; }
    public bool Grabbed { get; private set; }

    public Vector2 GrabPoint { get; private set; }
    public Vector2 GrabOffset { get; private set; }
    public Vector2 StartSelfAnchorPoint { get; private set; }

    public override CUIRect Rect
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

      Host.ResizeFrom2Points(
        CUIAnchor.PosFromAnchor(Host.Rect, StaticPointAnchor), StaticPointAnchor,
        SelfAnchorPoint, ParentAnchor
      );
    }

    private void Release(CUIMouseEvent e)
    {
      Vector2 delta = e.Pos - GrabPoint;
      Vector2 SelfAnchorPoint = StartSelfAnchorPoint + delta;

      Host.ResizeFrom2Points(
        CUIAnchor.PosFromAnchor(Host.Rect, StaticPointAnchor), StaticPointAnchor,
        SelfAnchorPoint, ParentAnchor
      );


      Grabbed = false;
      host.HubMouseMoved -= Update;
      host.HubMouseUp -= Release;
      host.Release(this);
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (Visible) yield return new VisualUnit.PrimitiveVisualElement(Background);
    }

    public VisualUnit.NestedVisualComponent SelfWrapper { get; }

    public ResizeHandle()
    {
      SelfWrapper = new VisualUnit.NestedVisualComponent(this);
      Background.Color = Color.Yellow;

      Anchor = new Vector2(1, 1);

      Background.MouseDown.Add(Grab);
    }
  }
}