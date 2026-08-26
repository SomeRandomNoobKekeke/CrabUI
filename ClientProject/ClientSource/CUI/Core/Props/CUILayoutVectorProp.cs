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


namespace CursedUI
{
  public class CUILayoutVectorProp : CUILayoutProp<Vector2>
  {
    private CUIBoundaries _Bounds; public CUIBoundaries Bounds
    {
      get => _Bounds;
      set
      {
        _Bounds = value;
        // Value = Value; //TODO rethink, this triggers infinite recalc loop
      }
    }

    public override Vector2 Value
    {
      get => base.Value;
      set
      {
        base.Value = Bounds.Check(value);
      }
    }
  }
}