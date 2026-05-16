using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
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

    public void Dispatch(EventTargets targets, List<InputEvent> events)
    {
      for (int i = 0; i < targets.Targets.Count; i++)
      {
        Dispatch(targets.Targets[i], events);
      }
    }


  }
}