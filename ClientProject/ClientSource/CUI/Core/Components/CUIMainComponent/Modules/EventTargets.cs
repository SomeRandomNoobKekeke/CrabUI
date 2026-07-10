using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using CUICodeGenerator;

namespace CrabUI
{
  public class EventTargets : IModule
  {
    public List<IEventConsumer> PrevTargets { get; private set; } = new();
    public List<IEventConsumer> Targets { get; private set; } = new();
    public IEventConsumer TopTarget { get; private set; }

    public void Find(List<VisualUnit> flat, Vector2 mousePos)
    {
      PrevTargets = Targets;
      Targets = new List<IEventConsumer>();

      Vector2 pos = mousePos;

      for (int i = flat.Count - 1; i >= 0; i--)
      {
        switch (flat[i])
        {
          case VisualUnit.PrimitiveVisualElement primitive:
            if (primitive.Element is IEventConsumer && primitive.Element.Contains(pos))
            {
              Targets.Add(primitive.Element as IEventConsumer);
            }

            break;
          case VisualBounds.LeftContextBound left:
            // leave context
            break;
          case VisualBounds.RightContextBound right:
            // enter context
            break;
          default:
            throw new Exception("Unexpected VisualUnit");
            break;
        }
      }

      TopTarget = Targets.ElementAtOrDefault(0);
    }

  }
}