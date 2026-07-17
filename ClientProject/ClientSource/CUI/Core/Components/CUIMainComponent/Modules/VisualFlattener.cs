using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

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
            case VisualBounds.LeftContextBound left:
              Flat.Add(left);
              break;
            case VisualBounds.RightContextBound right:
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