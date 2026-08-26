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
  /// I just wanna know if i can unsubscribe instance method from event
  /// It's easier to just check
  /// </summary>
  public class InstanceEventHandlers : Experiment
  {

    public event Action Bruh;

    public void BruhResponse()
    {
      Mod.Logger.Log("Bruh");
    }

    public override void Run()
    {
      // This works
      Bruh += BruhResponse;
      Bruh?.Invoke();
      Bruh -= BruhResponse;
      Bruh?.Invoke(); // nothing here

      // This doesn't
      // Bruh += () => BruhResponse();
      // Bruh?.Invoke();
      // Bruh -= () => BruhResponse();
      // Bruh?.Invoke(); // still running
    }
  }
}