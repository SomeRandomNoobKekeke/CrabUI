using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CrabUIUser
{

  /// <summary>
  /// This feels easy to fuck up
  /// </summary>
  public class UnsubscribingFromEvents : Experiment
  {

    public class EventWrapper
    {
      private event Action TheEvent;

      public void Raise() => TheEvent?.Invoke();
      public static EventWrapper operator +(EventWrapper e, Action callback)
      {
        e.TheEvent += callback;
        return e;
      }

      public static EventWrapper operator -(EventWrapper e, Action callback)
      {
        e.TheEvent -= callback;
        return e;
      }
    }

    public class Handle
    {
      private Component _host;
      public Component Host
      {
        get => _host;
        set
        {
          _host = value;
          Mod.Logger.Print($"Handle Host = [{value}]", Color.Pink);
          Resubscribe();
        }
      }

      public void Grab()
      {
        Mod.Logger.Log("Handle Grabbed");
      }
      public void Release()
      {
        Mod.Logger.Log("Handle Release");
      }

      private string _grabEventName = "MouseDown";
      public string GrabEventName
      {
        get => _grabEventName;
        set
        {
          _grabEventName = value;
          Mod.Logger.Print($"Handle GrabEventName = [{value}]", Color.Pink);
          Resubscribe();
        }
      }
      private string _releaseEventName = "MouseUp";
      public string ReleaseEventName
      {
        get => _releaseEventName;
        set
        {
          _releaseEventName = value;
          Mod.Logger.Print($"Handle ReleaseEventName = [{value}]", Color.Pink);
          Resubscribe();
        }
      }

      private EventWrapper PrevGrabEvent;
      private EventWrapper PrevReleaseEvent;


      private void Resubscribe()
      {
        if (PrevGrabEvent is not null) PrevGrabEvent -= Grab;
        if (PrevReleaseEvent is not null) PrevReleaseEvent -= Release;

        if (Host is not null)
        {
          if (Host.Events.ContainsKey(GrabEventName))
          {

            Host.Events[GrabEventName] += Grab;
            PrevGrabEvent = Host.Events[GrabEventName];
          }

          if (Host.Events.ContainsKey(ReleaseEventName))
          {
            Host.Events[ReleaseEventName] += Release;
            PrevReleaseEvent = Host.Events[ReleaseEventName];
          }
        }
      }
    }

    public class Component
    {
      public EventWrapper MouseDown = new EventWrapper();
      public EventWrapper MouseUp = new EventWrapper();
      public EventWrapper MouseClick = new EventWrapper();

      public Dictionary<string, EventWrapper> Events;

      public void RaiseEvents()
      {
        MouseDown.Raise();
        MouseUp.Raise();
        MouseClick.Raise();
      }


      public Component()
      {
        Events = new()
        {
          ["MouseDown"] = MouseDown,
          ["MouseUp"] = MouseUp,
          ["MouseClick"] = MouseClick,
        };
      }
    }

    public override void Run()
    {
      Handle handle = new();
      Component component = new Component();

      component.MouseDown += () => Mod.Logger.Print("MouseDown", Color.Green);
      component.MouseUp += () => Mod.Logger.Print("MouseUp", Color.Green);
      component.MouseClick += () => Mod.Logger.Print("MouseClick", Color.Green);

      handle.Host = component;
      component.RaiseEvents();

      handle.Host = null;
      component.RaiseEvents();

      handle.Host = component;
      handle.GrabEventName = "MouseClick";
      component.RaiseEvents();

      handle.Host = null;
      component.RaiseEvents();
    }
  }
}