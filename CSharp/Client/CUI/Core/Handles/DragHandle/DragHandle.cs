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
  public class DragHandle : IModule, IAware
  {
    public object HostComponent { get; set; }
    public string HostPropName { get; set; }

    private IDraggable host;
    [In]
    public IDraggable Host
    {
      get => host;
      set
      {
        if (host is not null) DisconnectFromHost(host);

        host = value;
        if (host is not null) ConnectToHost(host);
      }
    }


    private void ConnectToHost(IDraggable host)
    {
      host.MouseDown += Grab;
    }

    private void DisconnectFromHost(IDraggable host)
    {
      host.MouseDown -= Grab;
    }

    public bool Active { get; set; }
    public bool Grabbed { get; private set; }
    public Vector2 GrabOffset { get; private set; }


    private void Grab(CUIMouseEvent e)
    {
      if (!Active) return;
      if (!Host.TryGrab(this)) return;

      Grabbed = true;
      GrabOffset = Host.Rect.LeftTop - e.Pos;
      host.HubMouseMoved += Update;
      host.HubMouseUp += Release;
    }

    private void Release(CUIMouseEvent e)
    {
      Vector2 origin = e.Pos + GrabOffset - (Host.ParentRect?.LeftTop ?? Vector2.Zero);
      Host.SetLeftTopPos(origin.X, origin.Y);
      Grabbed = false;
      host.HubMouseMoved -= Update;
      host.HubMouseUp -= Release;
      host.Release(this);
    }

    public void Update(CUIMouseEvent e)
    {
      Vector2 origin = e.Pos + GrabOffset - (Host.ParentRect?.LeftTop ?? Vector2.Zero);
      Host.SetLeftTopPos(origin.X, origin.Y);
    }


  }
}