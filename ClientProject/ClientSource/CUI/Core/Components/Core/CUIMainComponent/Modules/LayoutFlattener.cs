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
  //TODO untangle from CUIVisualComponent?
  public class LayoutFlattener : IModule
  {
    public List<CUIVisualComponent> Flat { get; } = new();

    public void Flatten(CUIVisualComponent root)
    {
      Flat.Clear();

      void FlattenRec(CUIVisualComponent component)
      {
        Flat.Add(component);

        foreach (CUIVisualComponent child in component.StructuralSplit())
        {
          FlattenRec(child);
        }
      }

      FlattenRec(root);
    }
  }

}