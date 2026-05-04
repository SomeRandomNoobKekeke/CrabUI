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
    public event Action<CUIMouseDownEvent> MouseDown
    {
      add => this.Events.MouseDown.Add(value);
      remove => this.Events.MouseDown.Remove(value);
    }

    public event Action<CUIMouseUpEvent> MouseUp
    {
      add => this.Events.MouseUp.Add(value);
      remove => this.Events.MouseUp.Remove(value);
    }

    public event Action<CUIMouseClickEvent> MouseClick
    {
      add => this.Events.MouseClick.Add(value);
      remove => this.Events.MouseClick.Remove(value);
    }

    public event Action<CUIMouseDoubleClickEvent> MouseDoubleClick
    {
      add => this.Events.MouseDoubleClick.Add(value);
      remove => this.Events.MouseDoubleClick.Remove(value);
    }

    public event Action<CUIMouseMovedEvent> MouseMoved
    {
      add => this.Events.MouseMoved.Add(value);
      remove => this.Events.MouseMoved.Remove(value);
    }
  }
}