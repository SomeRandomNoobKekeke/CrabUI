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
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public partial class Access_Part : Part
    {
      protected Layout_Access Layout { get; } = new();
      public partial class Layout_Access : Part, Layout.Target
      {
        public void Init()
        {
          PlainLayout_Init();
        }

        CUIRect Layout.Target.Rect
        {
          get => Self.FunnyProps.Rect.Value;
          set => Self.FunnyProps.Rect.Value = value;
        }
      }

      public partial class Layout_Access : PlainLayout.Target
      {
        public void PlainLayout_Init()
        {
          // PlainLayout_Children = new ListProxy<PlainLayout.Target>(Host.Tree.Children);
        }
        CUINullRect PlainLayout.Target.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect PlainLayout.Target.Relative => Self.LayoutProps.Relative.Value;
        private IReadOnlyList<PlainLayout.Target> PlainLayout_Children;
        IReadOnlyList<PlainLayout.Target> PlainLayout.Target.Children => PlainLayout_Children;
      }
    }
  }
}