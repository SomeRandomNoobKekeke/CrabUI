using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;


namespace CrabUI
{
  public class DebugGate<T1> : DebugNode<T1> { }

  public class DebugGate<T1, T2> : DebugNode<T1, T2> { }
}