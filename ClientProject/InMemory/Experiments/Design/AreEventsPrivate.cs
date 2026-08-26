using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{

  /// <summary>
  /// I just wanna check, i don't get compile time errors in this environment
  /// </summary>
  public class AreEventsPrivate : Experiment
  {

    public class A
    {
      public event Action SomeEvent;
      protected void RaiseSomeEvent() => SomeEvent?.Invoke();
    }

    public class B : A
    {
      public void InvokeEvent()
      {
        // SomeEvent?.Invoke(); // attempt to access field failed
        RaiseSomeEvent();
      }
    }

    public override void Run()
    {
      B b = new B();
      b.SomeEvent += () => Mod.Logger.Log(123);
      b.InvokeEvent();
    }
  }
}