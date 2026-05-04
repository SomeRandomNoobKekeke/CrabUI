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
        public void PlainLayout_Init()
        {
          //CRINGE i can't target ReadOnlyChildren because IReadOnlyList doesn't implement IList
          PlainLayout_Children = new ListProxy<CUIComponent, PlainLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );
        }
        CUINullRect PlainLayout.Target.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect PlainLayout.Target.Relative => Self.LayoutProps.Relative.Value;
        private IReadOnlyList<PlainLayout.Target> PlainLayout_Children;
        IReadOnlyList<PlainLayout.Target> PlainLayout.Target.Children => PlainLayout_Children;
      }
    }
  }
}