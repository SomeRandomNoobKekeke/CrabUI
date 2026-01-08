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
    public void Dispatch(List<IEventConsumer> targets, List<InputEvent> events)
    {
      foreach (IEventConsumer target in targets)
      {
        foreach (InputEvent e in events)
        {
          e.Dispatch(target);
        }
      }
    }
  }
}