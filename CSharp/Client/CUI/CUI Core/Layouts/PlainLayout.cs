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
      foreach (IBasicLayoutElement child in Children)
      {
        // if (child.Relative.HasValue)
        // {
        //   child.Rect = child.Absolute;
        // }

        if (child.Absolute.HasValue)
        {
          child.Rect = child.Absolute.Value;
          CUI.Logger.Log($"{child}.Rect = {child.Rect}");
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