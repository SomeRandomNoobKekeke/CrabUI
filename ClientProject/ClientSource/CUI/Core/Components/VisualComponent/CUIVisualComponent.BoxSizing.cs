using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    public abstract CUIRect OuterRect { get; set; }
    // public abstract CUIRect Rect { get; set; }
    // public abstract CUIRect InnerRect { get; set; }
    public abstract CUIRect ChildrenRect { get; set; }

    public CUISizes OutToChildDiff { get; protected set; }
  }
}