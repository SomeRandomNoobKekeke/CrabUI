using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;

namespace CrabUI
{
  public interface IDragHandleHub
  {
    public event Action<CUIMouseUpEvent> MouseUp;
    public event Action<CUIMouseMovedEvent> MouseMoved;
  }
}