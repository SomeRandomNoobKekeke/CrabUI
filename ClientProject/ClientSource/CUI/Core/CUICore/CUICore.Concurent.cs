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
    public static Debouncer Debouncer => Instance?._Debouncer;
    private Debouncer _Debouncer = new();
  }
}