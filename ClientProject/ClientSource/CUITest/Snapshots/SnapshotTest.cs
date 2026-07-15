using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public record SnapshotTest(Func<CUIComponent> TestFunc, string Name, string Group)
  {
    public static SnapshotTest FromMethodInfo(MethodInfo mi)
      => new SnapshotTest(
        (Func<CUIComponent>)Delegate.CreateDelegate(typeof(Func<CUIComponent>), mi),
        $"{mi.DeclaringType.Name}.{mi.Name}",
        $"{mi.DeclaringType.Name}"
      );
  }
}