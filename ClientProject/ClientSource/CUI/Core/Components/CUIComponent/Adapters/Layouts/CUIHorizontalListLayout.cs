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
      public partial class Layout_Adapter : CUIHorizontalListLayout.Target
      {
        CUINullRect CUIHorizontalListLayout.Target.Absolute
          => Self.LayoutProps.Absolute.Value;
        CUINullRect CUIHorizontalListLayout.Target.Relative
          => Self.LayoutProps.Relative.Value;
        CUIDirection CUIHorizontalListLayout.Target.Direction
          => Self.LayoutProps.Direction.Value;

        float? CUIHorizontalListLayout.Target.Flex => Self.LayoutProps.Flex.Value;

        IReadOnlyList<CUIHorizontalListLayout.Target> CUIHorizontalListLayout.Target.Children
          => new ListProxy<CUIComponent, CUIHorizontalListLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );
      }
    }
  }
}