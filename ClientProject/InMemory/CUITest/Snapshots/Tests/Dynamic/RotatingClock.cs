using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Dynamic
    {
      public static CUIComponent RotatingClock()
      {
        DynamicContainer container = new DynamicContainer()
        {
          Children = {AddBulk = Enumerable.Range(1,12).Select(i=>new CUITextBlock($"{i}")
          {
            Anchor = CUIAnchor.Center,
          })},
        };

        double angle = Math.PI * 2 / container.Children.Count;
        double speed = 0.5;

        container.Updated += () =>
        {
          for (int i = 0; i < container.Children.Count; i++)
          {
            container[i].Absolute = container[i].Absolute with
            {
              Position = new Vector2(
                (float)Math.Cos((i * angle + Timing.TotalTime * speed)) * 100,
                (float)Math.Sin((i * angle + Timing.TotalTime * speed)) * 100
              )
            };
          }
        };

        return container;
      }
    }
  }
}