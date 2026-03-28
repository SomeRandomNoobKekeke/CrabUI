using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public partial class Access_Part : Part
    {
      // public IDragHandleHub_Access IDragHandleHub { get; } = new();
      // public partial class IDragHandleHub_Access : Part, IAccess, IDragHandleHub
      // {
      //   public event Action<CUIMouseUpEvent> MouseUp
      //   {
      //     add => Self.GlobalEvents.MouseUp.Add(value);
      //     remove => Self.GlobalEvents.MouseUp.Remove(value);
      //   }
      //   public event Action<CUIMouseMovedEvent> MouseMoved
      //   {
      //     add => Self.GlobalEvents.MouseMoved.Add(value);
      //     remove => Self.GlobalEvents.MouseMoved.Remove(value);
      //   }
      // }
    }
  }
}