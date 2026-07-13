using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;
using BaroJunk;

namespace CrabUIUser
{


  public partial class HowLayoutsShouldGenerallyWork : Experiment
  {
    public class ListComponent : Component
    {
      public class Part { public ListComponent Self { get; set; } }
      public ListComponent() : base()
      {
        SetupLayout();
      }

      public bool ResizeChildrenToHost { get; set; }


      public partial class LayoutAdapter_Part : Part, ListLayout.Host
      {
        IReadOnlyList<Layout.Child> Layout.Host.Children
          => Self.Children.As<Component, Layout.Child>(c => c.AsLayoutChild);
        // => new ListProxy<Component, Layout.Child>(
        //     Self.Children,
        //     c => c.AsLayoutChild as Layout.Child
        //   );

        Rectangle Layout.Host.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }
        bool ListLayout.Host.ResizeChildrenToHost => Self.ResizeChildrenToHost;
      }

      public override void SetupLayout()
      {
        RealLayout = new ListLayout();
        Layout = RealLayout;
        RealLayout.ConnectTo(new LayoutAdapter_Part() { Self = this });
      }

      private ListLayout RealLayout;
    }



  }
}