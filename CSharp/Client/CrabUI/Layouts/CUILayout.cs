using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Base class for all layouts
  /// </summary>
  public class CUILayout
  {
    public CUIComponent Host;


    //NOTE: This looks ugly, but no matter how i try to isolate this logic it gets only uglier
    // i've been stuck here for too long so i'll just do this
    // and each update pattern in fact used only once, so i think no big deal

    /// <summary>
    /// This is just for debug, don't use it
    /// </summary>
    public void ForceMarkUnchanged()
    {
      decorChanged = false;
      changed = false;
      absoluteChanged = false;

      foreach (CUIComponent child in Host.Children)
      {
        child.Layout.ForceMarkUnchanged();
      }
    }

    private void propagateChangedDown()
    {
      changed = true;
      DecorChanged = true;
      foreach (CUIComponent child in Host.Children)
      {
        child.Layout.propagateChangedDown();
      }
    }
    protected bool changed = true; public bool Changed
    {
      get => changed;
      set
      {
        changed = value;
        if (value)
        {
          if (Host.Parent != null) Host.Parent.Layout.propagateChangedDown();
          else propagateChangedDown();
        }
      }
    }

    private void propagateDecorChangedDown()
    {
      DecorChanged = true;
      foreach (CUIComponent child in Host.Children)
      {
        child.Layout.propagateDecorChangedDown();
      }
    }

    /// <summary>
    /// It doesn't optimize anything
    /// </summary>
    public bool SelfAndParentChanged
    {
      set
      {
        if (value)
        {
          changed = true;
          DecorChanged = true;
          if (Host.Parent != null)
          {
            Host.Parent.Layout.changed = true;
            Host.Parent.Layout.propagateDecorChangedDown();
          }
        }
      }
    }

    public bool ChildChanged
    {
      set
      {
        if (value) propagateChangedDown();
      }
    }

    private void propagateAbsoluteChangedUp()
    {
      absoluteChanged = true;
      Host.Parent?.Layout.propagateAbsoluteChangedUp();
    }
    protected bool absoluteChanged = true; public bool AbsoluteChanged
    {
      get => absoluteChanged;
      set
      {
        if (!value) absoluteChanged = false;
        if (value && Host.Parent != null) Host.Parent.Layout.absoluteChanged = true;
        //if (value && Host.Parent != null) Host.Parent.Layout.propagateAbsoluteChangedUp();
      }
    }
    protected bool decorChanged = true; public bool DecorChanged
    {
      get => decorChanged;
      set
      {
        decorChanged = value;
      }
    }

    internal virtual void Update()
    {
      if (Changed)
      {
        if (Host.CullChildren)
        {
          foreach (CUIComponent c in Host.Children)
          {
            c.CulledOut = !c.UnCullable && !c.Real.Intersect(Host.Real);
          }
        }

        Changed = false;
      }
    }

    internal virtual void UpdateDecor()
    {
      if (DecorChanged)
      {
        Host.UpdatePseudoChildren();
        DecorChanged = false;
      }
    }

    internal virtual void ResizeToContent()
    {
      if (AbsoluteChanged && (Host.FitContent.X || Host.FitContent.Y))
      {
        // do something
      }

      AbsoluteChanged = false;
    }

    public CUILayout() { }
    public CUILayout(CUIComponent host)
    {
      Host = host;
    }

    public override string ToString() => this.GetType().Name;
    public static CUILayout Parse(string raw)
    {
      if (raw != null)
      {
        raw = raw.Trim();
        if (CUIReflection.CUILayoutTypes.ContainsKey(raw))
        {
          return (CUILayout)Activator.CreateInstance(CUIReflection.CUILayoutTypes[raw]);
        }
      }
      return new CUILayoutSimple();
    }
  }
}