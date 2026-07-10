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
  //TODO untangle from CUIComponent?
  public class LayoutFlattener : IModule
  {
    public List<CUIComponent> Flat { get; } = new();

    public void Flatten(CUIComponent root)
    {
      Flat.Clear();

      void FlattenRec(CUIComponent component)
      {
        Flat.Add(component);

        foreach (CUIComponent child in component.Children)
        {
          FlattenRec(child);
        }
      }

      FlattenRec(root);
    }
  }

}