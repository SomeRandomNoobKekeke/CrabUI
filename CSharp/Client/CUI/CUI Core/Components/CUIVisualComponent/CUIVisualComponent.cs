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
  public abstract partial class CUIVisualComponent : CUIComponentCore, IVisualComponent
  {

    IEnumerable<VisualUnit> IVisualComponent.VisualSplit() => VisualSplit();
    protected abstract IEnumerable<VisualUnit> VisualSplit();


    Rectangle IVisualComponent.Rect
    {
      get => Rect;
      set => Rect = value;
    }
    protected abstract Rectangle Rect { get; set; }
  }
}