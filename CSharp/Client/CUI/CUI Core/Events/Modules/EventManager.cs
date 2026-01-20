using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  //Looks redundant
  public class EventManager : IModule
  {
    public EventConstructor Constructor;
    public EventDispatcher Dispatcher;

    public void Dispatch() { }
  }
}