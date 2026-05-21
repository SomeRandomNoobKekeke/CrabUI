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
      public partial class Layout_Adapter : PlainLayout.Target
      {
        CUIRect PlainLayout.Target.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }

        CUINullRect PlainLayout.Target.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect PlainLayout.Target.Relative => Self.LayoutProps.Relative.Value;

        Vector2 PlainLayout.Target.Anchor => Self.LayoutProps.Anchor.Value;
        Vector2? PlainLayout.Target.ParentAnchor => Self.LayoutProps.ParentAnchor.Value;
        Vector2 PlainLayout.Target.ChildrenOffset => Self.LayoutProps.RealChildrenOffset;
        IReadOnlyList<PlainLayout.Target> PlainLayout.Target.Children
          => new ListProxy<CUIComponent, PlainLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );


      }
    }
  }
}