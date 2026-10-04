using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CursedUI
{
  public class CUIDebugEvent : DebugEvent
  {
    public CUIVisualComponent RelatedComponent { get; set; }
  }
}