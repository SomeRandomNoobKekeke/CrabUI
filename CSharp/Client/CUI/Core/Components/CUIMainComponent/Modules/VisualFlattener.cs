using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public class VisualFlattener : IModule
  {
    public List<VisualUnit> Flat { get; } = new();

    public void Flatten(IVisualComponent root)
    {
      Flat.Clear();

      void FlattenRec(IVisualComponent component)
      {
        foreach (VisualUnit unit in component.VisualSplit())
        {
          switch (unit)
          {
            case VisualUnit.PrimitiveVisualElement primitive:
              Flat.Add(primitive);
              break;
            case VisualUnit.LeftContextBound left:
              Flat.Add(left);
              break;
            case VisualUnit.RightContextBound right:
              Flat.Add(right);
              break;
            case VisualUnit.NestedVisualComponent nested:
              FlattenRec(nested.Component);
              break;
            default:
              throw new Exception("Unexpected VisualFlattenerInstruction");
              break;
          }
        }
      }

      FlattenRec(root);
    }
  }
}