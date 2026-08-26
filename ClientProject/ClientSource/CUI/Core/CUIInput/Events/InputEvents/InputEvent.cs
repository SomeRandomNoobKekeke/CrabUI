using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public abstract class InputEvent
  {
    public bool Consumed { get; set; } = false;
    public abstract void Dispatch(IEventConsumer consumer);
  }
}