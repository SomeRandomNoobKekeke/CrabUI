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
  /// Somehow list calls even a bit faster
  /// </summary>
  public class EventsVsListInvoke : Experiment
  {

    public int value;
    public int repeats = 1000000;
    public int eventCount = 100;

    public class EventWrapper
    {
      public event Action DoIt;
      public void Raise() => DoIt.Invoke();
    }

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();


      List<Action> list = new();
      for (int i = 0; i < eventCount; i++)
      {
        list.Add(() => value = 123);
      }

      EventWrapper wrapper = new();
      for (int i = 0; i < eventCount; i++)
      {
        wrapper.DoIt += () => value = 123;
      }


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        for (int j = 0; j < list.Count; j++)
        {
          list[j].Invoke();
        }
      }
      sw.Stop();
      Mod.Logger.Log($"list calls: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        wrapper.Raise();
      }
      sw.Stop();
      Mod.Logger.Log($"event calls: [{sw.ElapsedMilliseconds}]");

    }
  }
}