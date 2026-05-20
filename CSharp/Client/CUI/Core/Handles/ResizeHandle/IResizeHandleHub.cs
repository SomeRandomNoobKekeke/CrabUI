using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public interface IResizeHandleHub
  {
    public event Action<CUIMouseUpEvent> MouseUp;
    public event Action<CUIMouseMovedEvent> MouseMoved;
  }
}