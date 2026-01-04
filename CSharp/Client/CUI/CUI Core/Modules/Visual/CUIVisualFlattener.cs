using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIVisualFlattener : IModule
  {
    public List<VI.VisualUnit> Flat { get; } = new();

    public void Flatten(IVisualComponent root)
    {
      Flat.Clear();

      void FlattenRec(IVisualComponent component)
      {
        foreach (VI.VisualFlattenerInstruction instruction in component.VisualSplit())
        {
          switch (instruction)
          {
            case VI.PrimitiveVisualElement primitive:
              Flat.Add(primitive);
              break;
            case VI.LeftContextBound left:
              Flat.Add(left);
              break;
            case VI.RightContextBound right:
              Flat.Add(right);
              break;
            case VI.NestedVisualComponent nested:
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