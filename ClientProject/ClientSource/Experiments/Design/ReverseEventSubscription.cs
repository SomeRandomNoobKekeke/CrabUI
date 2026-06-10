using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class ReverseEventSubscription : Experiment
  {
    public abstract class EventWrapperBase
    {
      public abstract void Map(Delegate callback);
    }

    public class EventWrapper<T> : EventWrapperBase
    {
      public event Action<T> Event;
      public override void Map(Delegate callback) => Map((Action<T>)callback);
      public void Map(Action<T> callback) => Event += callback;

      public void Raise(T arg) => Event?.Invoke(arg);

      public void Route(EventWrapperBase eventWrapper, Delegate callback)
      {
        eventWrapper.Map(callback);
      }
    }


    public EventWrapper<int> event1 = new();
    public EventWrapper<string> event2 = new();

    public override void Run()
    {
      event2.Map(s => Mod.Logger.Log($"Routing {s}"));

      event1.Map((i) => event2.Raise($"Direct [{i}]")); // Direct
      event2.Route(event1, (int i) => event2.Raise($"Reverse [{i}]")); // Reverse

      event1.Raise(123);
    }
  }



}