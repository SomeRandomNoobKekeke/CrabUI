using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class LayoutMarker : IModule
  {
    public interface Target : IModule
    {
      public Target Parent { get; }
      public Layout Layout { get; }
      public IEnumerable<Target> Children { get; }
      public void NotifyLayoutUpdated();
    }

    public CUIDebugNode<Target, Pattern> Debug_Marking_Start = new(DebugCategory.LayoutMarked)
    {
      MsgFactory = (host, pattern) => $"[{host}] ... marking with [{pattern}]"
    };

    public CUIDebugNode<Target, Pattern> Debug_Marking_End = new(DebugCategory.LayoutMarked)
    {
      MsgFactory = (host, pattern) => $"[{host}] ... finished marking with [{pattern}]"
    };

    [In] public Target Host { get; set; }

    public void Mark(Pattern pattern)
    {
      if (pattern.Empty) return;

      Debug_Marking_Start.Send(Host, pattern);
      pattern.MarkFunc(Host);
      Debug_Marking_End.Send(Host, pattern);

      Host.NotifyLayoutUpdated();

    }
  }
}