using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public partial class CUICore
  {
    private DebugHub _DebugHub; public DebugHub DebugHub
    {
      get
      {
        if (_DebugHub is null) _DebugHub = new();
        return _DebugHub;
      }
    }
  }
}