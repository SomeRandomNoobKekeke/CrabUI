using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace bruh
{

  public class Flattener
  {
    public List<VisualInstruction> Flat = new();

    public void Flatten(Component component)
    {
      Flat.Clear();

      void FlattenRec(IVisualComponent c)
      {
        Flat.Add(new VisualContextLeft(c.Context));

        foreach (IVisualElement e in c.Elements())
        {
          if (e is IVisualComponent child)
          {
            FlattenRec(child);
          }
          else
          {
            Flat.Add(e);
          }
        }

        Flat.Add(new VisualContextRight(c.Context));
      }

      FlattenRec(component);
    }
  }
}
