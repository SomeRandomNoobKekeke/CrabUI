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
  public partial class CUIComponent
  {
    public class LayoutFlattener : CUIComponent.Part, IModule
    {
      public List<CUIComponent> Flat { get; } = new();

      public void Flatten(CUIComponent root)
      {
        Flat.Clear();

        void FlattenRec(CUIComponent component)
        {
          Flat.Add(component);

          foreach (CUIComponent child in component.Tree.Children)
          {
            FlattenRec(child);
          }
        }

        FlattenRec(root);
      }
    }
  }

}