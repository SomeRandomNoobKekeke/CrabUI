using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected LayoutProps_Part LayoutProps { get; } = new();
    public class LayoutProps_Part : Part, ICUILayoutProp.IContainer
    {
      public DebugNode<Type, string, object> LayoutPropSet { get; } = new();

      public void Init()
      {
        var PropSetChannel = Self.DebugChannels.Get<DebugNode<CUIComponent, Type, string, object>>("Prop Set");



        LayoutPropSet.Map(
          PropSetChannel,
          (Type t, string s, object o) => PropSetChannel.Send(Self, t, s, o)
        );

        Absolute.DebugValueSet.Map(LayoutPropSet);
        Relative.DebugValueSet.Map(LayoutPropSet);
      }

      void ICUILayoutProp.IContainer.Mark(LayoutMarker.Pattern pattern)
      {
        Self.LayoutMarker.Mark(pattern);
      }

      public CUILayoutProp<CUINullRect> Absolute { get; set; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };
      public CUILayoutProp<CUINullRect> Relative { get; set; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };
    }
  }
}