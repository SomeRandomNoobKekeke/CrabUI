using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public class EventDispatcher : IModule
  {
    public void Dispatch(IEventConsumer consumer, InputEvent inputEvent)
    {
      if (inputEvent.Consumed) return;
      inputEvent.Dispatch(consumer);
    }

    public void Dispatch(IEventConsumer consumer, IEnumerable<InputEvent> events)
    {
      foreach (InputEvent e in events)
      {
        if (e.Consumed) continue;
        e.Dispatch(consumer);
      }
    }

    public void Dispatch(IEnumerable<IEventConsumer> targets, List<InputEvent> events)
    {
      foreach (IEventConsumer consumer in targets)
      {
        Dispatch(consumer, events);
      }
    }

    public void Dispatch(IEnumerable<IEventConsumer> targets, InputEvent inputEvent)
    {
      foreach (IEventConsumer consumer in targets)
      {
        if (inputEvent.Consumed) return;
        inputEvent.Dispatch(consumer);
      }
    }


  }
}