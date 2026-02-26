using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{

  //TODO this seems working, but i need to ensure unsubscribing
  // perhaps CUIEvent should return subscription object, or support one time callbacks
  public class DragHandle : IModule
  {
    private CUIComponent _host;
    public CUIComponent Host
    {
      get => _host;
      set
      {
        _host = value;
        Install();
      }
    }

    public bool Active { get; set; }
    public bool Grabbed { get; private set; }
    public Vector2 GrabOffset { get; private set; }

    private void Install()
    {
      if (Host is not null)
      {
        Host.MouseDown -= Grab;
      }

      Host.MouseDown += Grab;
    }

    private void Grab(CUIMouseEvent e)
    {
      Grabbed = true;
      GrabOffset = Host.Rect.LeftTop - e.Pos;
      Host.MainComponent.GlobalEvents.MouseMoved += Update;
      Host.MainComponent.GlobalEvents.MouseUp += Release;
    }

    private void Release(CUIMouseEvent e)
    {
      Grabbed = false;
      Host.MainComponent.GlobalEvents.MouseMoved -= Update;
      Host.MainComponent.GlobalEvents.MouseUp -= Release;
    }

    public void Update(CUIMouseEvent e)
    {
      Vector2 origin = e.Pos + GrabOffset - (Host.Parent?.Rect.LeftTop ?? Vector2.Zero);
      Host.Absolute = new CUINullRect(
        origin.X, origin.Y,
        Host.Absolute.Width, Host.Absolute.Height
      );
    }


  }

}