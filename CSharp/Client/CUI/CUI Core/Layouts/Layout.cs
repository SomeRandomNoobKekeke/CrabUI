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
  public class Layout
  {
    public CUIVisualComponent Host;
    public IEnumerable<CUIVisualComponent> Children => Host.Children;


    public bool RequireChildrenUpdate { get; set; }
    public bool RequireParentUpdate { get; set; }




    public virtual void UpdateChildren()
    {
      RequireChildrenUpdate = false;
    }

    public virtual void UpdateParent()
    {
      RequireParentUpdate = false;
    }

    public Layout(CUIVisualComponent host) => Host = host;
  }
}