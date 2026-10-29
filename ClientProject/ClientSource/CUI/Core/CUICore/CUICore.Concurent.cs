using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUICore
  {
    public static void Debounce(Action action, int delay = 300)
    {
      Instance?._Debouncer.Debounce(action, delay);
    }
    private Debouncer _Debouncer = new();
  }
}