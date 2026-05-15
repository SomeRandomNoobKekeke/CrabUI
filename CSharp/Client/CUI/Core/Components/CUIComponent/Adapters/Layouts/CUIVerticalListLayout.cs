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
      public partial class Layout_Adapter : CUIVerticalListLayout.Target
      {
        CUIRect CUIVerticalListLayout.Target.Rect
        {
          get => Self.FunnyProps.Rect.Value;
          set => Self.FunnyProps.Rect.Value = value;
        }
        CUINullRect CUIVerticalListLayout.Target.Absolute
          => Self.LayoutProps.Absolute.Value;
        CUINullRect CUIVerticalListLayout.Target.Relative
          => Self.LayoutProps.Relative.Value;
        CUIDirection CUIVerticalListLayout.Target.Direction
          => Self.LayoutProps.Direction.Value;

        float? CUIVerticalListLayout.Target.Flex => Self.LayoutProps.Flex.Value;

        IReadOnlyList<CUIVerticalListLayout.Target> CUIVerticalListLayout.Target.Children
          => new ListProxy<CUIComponent, CUIVerticalListLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );
      }
    }
  }
}