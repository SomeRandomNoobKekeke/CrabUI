using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part : Part
    {
      public Layout_Adapter Layout { get; } = new();

      public partial class Layout_Adapter : Part, IAdapterPart, Layout.Target
      {
        CUIRect Layout.Target.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }

        bool Layout.Target.CullChildren => Self.CullChildren;
        bool Layout.Target.CulledOut
        {
          get => Self.CulledOut;
          set => Self.CulledOut = value;
        }

        IReadOnlyList<Layout.Target> Layout.Target.Children => new ListProxy<CUIComponent, Layout.Target>(
          Self.Tree.Children, c => c.Adapters.Layout
        );
      }
    }
  }
}