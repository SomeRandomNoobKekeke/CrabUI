using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class PlainLayout : Layout
  {


    public override void UpdateChildren()
    {
      if (!RequireChildrenUpdate) return;

      foreach (IBasicLayoutElement child in Children)
      {
        if (child.Absolute.HasValue)
        {
          child.Rect = child.Absolute.Value;
        }
      }


      RequireChildrenUpdate = false;
    }

    public override void UpdateParent()
    {
      RequireParentUpdate = false;
    }

    public PlainLayout(IBasicLayoutElement host, IReadOnlyList<IBasicLayoutElement> children) : base(host, children) { }

  }
}