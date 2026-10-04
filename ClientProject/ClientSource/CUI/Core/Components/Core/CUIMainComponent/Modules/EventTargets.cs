using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public class EventTargets : IModule
  {
    public CUIDebugNode<List<IEventConsumer>> Debug_Targets { get; } = new(DebugCategory.EventTargets)
    {
      IsOpen = true,
      MsgFactory = (targets) => $"[\n{String.Join(",\n", targets.Select(t =>
      {
        return t is IAware ? $"  [{(t as IAware).HostComponent}].{(t as IAware).HostPropName}" : $"  {t}";
      }))}\n]",
    };

    public List<IEventConsumer> PrevTargets { get; private set; } = new();
    public List<IEventConsumer> Targets { get; private set; } = new();
    public IEventConsumer TopTarget { get; private set; }

    public void Find(List<VisualUnit> flat, Vector2 mousePos)
    {
      PrevTargets = Targets;
      Targets = new List<IEventConsumer>();

      Vector2 pos = mousePos;

      VisualBounds? blockedBy = null;

      for (int i = 0; i < flat.Count; i++)
      {
        switch (flat[i])
        {
          case VisualUnit.PrimitiveVisualElement primitive:
            if (blockedBy != null) break;
            if (primitive.Element is IEventConsumer && primitive.Element.Contains(pos))
            {
              Targets.Add(primitive.Element as IEventConsumer);
            }

            break;
          case VisualBounds.LeftContextBound left:
            if (blockedBy != null) break;
            if (left.Bounds.ScissorRect.HasValue && !left.Bounds.ScissorRect.Value.Contains(pos))
            {
              blockedBy = left.Bounds;
            }
            break;
          case VisualBounds.RightContextBound right:
            if (right.Bounds == blockedBy)
            {
              blockedBy = null;
            }
            break;
          default:
            throw new Exception("Unexpected VisualUnit");
        }
      }

      // Scanning from parent to children then reversing to handle visual bounds correctly
      Targets.Reverse(); //TODO optimize

      TopTarget = Targets.ElementAtOrDefault(0);
      Debug_Targets.Send(Targets);
    }

  }
}