using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CrabUI
{
  public partial class CUICore
  {
    public DebugHub DebugHub { get; } = new()
    {
      IsOpen = false,
    };
  }
}