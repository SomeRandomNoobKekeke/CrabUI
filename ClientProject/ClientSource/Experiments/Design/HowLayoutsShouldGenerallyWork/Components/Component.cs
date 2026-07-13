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
    public class Component
    {
      public class Part { public Component Self { get; set; } }
      public Component()
      {
        AsLayoutChild.Self = this;
        SetupLayout();
      }



      public LayoutChildAdapter_Part AsLayoutChild { get; } = new();
      public partial class LayoutChildAdapter_Part : Part, Layout.Child
      {
        bool ListLayout.Child.Flex => Self.Flex;
        Rectangle PlainLayout.Child.Absolute => Self.Absolute;
        Rectangle Layout.ChildBase.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }
      }

      public partial class LayoutHostAdapter_Part : Part, PlainLayout.Host
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
      }


      public List<Component> Children { get; } = new();
      public Rectangle Rect { get; set; }
      public Rectangle Absolute { get; set; }
      public bool Flex { get; set; }

      public virtual void SetupLayout()
      {
        RealLayout = new PlainLayout();
        Layout = RealLayout;
        RealLayout.ConnectTo(new LayoutHostAdapter_Part() { Self = this });
      }
      public Layout Layout { get; set; }
      private PlainLayout RealLayout;
    }



  }
}