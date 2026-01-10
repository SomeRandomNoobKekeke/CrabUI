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
  public abstract class Layout
  {
    public IBasicLayoutElement Host;
    public IReadOnlyList<IBasicLayoutElement> Children;


    public bool RequireChildrenUpdate { get; set; } = true;
    public bool RequireParentUpdate { get; set; } = true;




    public virtual void UpdateChildren()
    {
      RequireChildrenUpdate = false;
    }

    public virtual void UpdateParent()
    {
      RequireParentUpdate = false;
    }

    public Layout(IBasicLayoutElement host, IReadOnlyList<IBasicLayoutElement> children)
    {
      Host = host;
      Children = children;
    }
  }
}