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
  /// Seems cheap
  /// </summary>
  public class ChainEventCalls : Experiment
  {
    public event Action event1;
    public event Action event2;
    public event Action event3;
    public event Action event4;

    public int value;
    public int repeats = 100_000_000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      Action setValue = () => value = 2;

      event4 += setValue;
      event3 += () => event4.Invoke();
      event2 += () => event3.Invoke();
      event1 += () => event2.Invoke();


      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        value = 2;
      }
      sw.Stop();
      Mod.Logger.Log($"direct call: [{sw.ElapsedMilliseconds}]");

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        setValue.Invoke();
      }
      sw.Stop();
      Mod.Logger.Log($"lambda: [{sw.ElapsedMilliseconds}]");


      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        event4.Invoke();
      }
      sw.Stop();
      Mod.Logger.Log($"1 event: [{sw.ElapsedMilliseconds}]");

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        event3.Invoke();
      }
      sw.Stop();
      Mod.Logger.Log($"2 events: [{sw.ElapsedMilliseconds}]");

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        event2.Invoke();
      }
      sw.Stop();
      Mod.Logger.Log($"3 events: [{sw.ElapsedMilliseconds}]");

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        event1.Invoke();
      }
      sw.Stop();
      Mod.Logger.Log($"4 events: [{sw.ElapsedMilliseconds}]");
    }
  }
}