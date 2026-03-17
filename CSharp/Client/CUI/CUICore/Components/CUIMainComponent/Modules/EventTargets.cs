using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class EventTargets : IModule
  {
    public List<IEventConsumer> Targets { get; } = new();
    public IEventConsumer TopTarget { get; private set; }

    public void Find(List<VisualUnit> flat, Vector2 mousePos)
    {
      Targets.Clear();

      Vector2 pos = mousePos;

      for (int i = flat.Count - 1; i >= 0; i--)
      {
        switch (flat[i])
        {
          case VisualUnit.PrimitiveVisualElement primitive:
            if (primitive.Element is IEventConsumer && primitive.Element.Rect.Contains(pos))
            {
              Targets.Add(primitive.Element as IEventConsumer);
            }

            break;
          case VisualUnit.LeftContextBound left:
            // leave context
            break;
          case VisualUnit.RightContextBound right:
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