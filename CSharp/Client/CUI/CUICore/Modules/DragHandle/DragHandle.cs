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
  public class DragHandle : IModule
  {
    [In] public IDragHandleHub Hub { get; set; }

    [In]
    public IDraggable Host
    {
      get => host;
      set
      {
        if (host is not null) DisconnectFromHost(host);

        host = value;
        CUI.Logger.LogVars(host);
        if (host is not null) ConnectToHost(host);
      }
    }
    private IDraggable host;

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
      Grabbed = true;
      GrabOffset = Host.Rect.LeftTop - e.Pos;
      Hub.MouseMoved += Update;
      Hub.MouseUp += Release;
    }

    private void Release(CUIMouseEvent e)
    {
      Grabbed = false;
      Hub.MouseMoved -= Update;
      Hub.MouseUp -= Release;
    }

    public void Update(CUIMouseEvent e)
    {
      Vector2 origin = e.Pos + GrabOffset - (Host.ParentRect?.LeftTop ?? Vector2.Zero);
      Host.SetAbsolutePos(origin.X, origin.Y);
    }


  }
}