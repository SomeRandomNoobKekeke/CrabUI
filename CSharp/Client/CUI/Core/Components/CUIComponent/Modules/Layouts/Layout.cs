using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public abstract class Layout : IModule
  {
    public interface Target
    {
      public CUIRect Rect { get; set; }
      public IList Children { get; }
    }

    public static InfoChannel<string> DebugLog = new()
    {
      Mapping = (msg) => CUI.Logger.Log(msg)
    };

    public virtual void InjectHost(Target host) { }

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
  }
}