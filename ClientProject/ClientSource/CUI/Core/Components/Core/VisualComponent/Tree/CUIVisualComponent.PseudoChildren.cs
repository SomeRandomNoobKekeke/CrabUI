using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Collections;
namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    /// <summary>
    /// Cursed, use it to make components that are not in Children but in StructuralSplit think that they have a parent
    /// </summary>
    protected void AttachChild(CUIVisualComponent child) => TreeOperations.AttachChild(child);

    /// <summary>
    /// Hacky fake dict for attaching childs without adding them to Children
    /// </summary>
    protected PseudoChildren_Part PseudoChildren { get; } = new();

    protected class PseudoChildren_Part : Part
    {
      public CUIVisualComponent this[string name]
      {
        get => Self[name];
        set
        {
          value.AKA = name;
          Self.AttachChild(value);
        }
      }
    }
  }
}