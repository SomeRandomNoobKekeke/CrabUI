using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class EventDispatcher : IModule
  {
    public void Dispatch(IEventConsumer consumer, IEnumerable<InputEvent> events)
    {
      foreach (InputEvent e in events)
      {
        if (e.Consumed) continue;
        e.Dispatch(consumer);
      }
    }

    public void Dispatch(EventTargets targets, EventConstructor eventConstructor)
    {
      for (int i = 0; i < targets.Targets.Count; i++)
      {
        Dispatch(targets.Targets[i], eventConstructor.Events);
      }
    }


  }
}