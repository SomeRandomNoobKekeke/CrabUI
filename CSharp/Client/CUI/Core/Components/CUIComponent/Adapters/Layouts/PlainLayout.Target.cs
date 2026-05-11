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
          get => Self.FunnyProps.Rect.Value;
          set => Self.FunnyProps.Rect.Value = value;
        }

        CUINullRect PlainLayout.Target.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect PlainLayout.Target.Relative => Self.LayoutProps.Relative.Value;

        IReadOnlyList<PlainLayout.Target> PlainLayout.Target.Children
          => new ListProxy<CUIComponent, PlainLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );
      }
    }
  }
}