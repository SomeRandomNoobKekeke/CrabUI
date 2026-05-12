using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public static partial class CUITestFactories
  {
    public static bool IsFactoryMethod(MethodInfo mi)
      => mi.ReturnType == typeof(CUIComponent) && mi.GetParameters().Length == 0;
    public static IEnumerable<MethodInfo> AllFactoryMethods()
      => typeof(CUITestFactories).GetMethods(BindingFlags.Public | BindingFlags.Static)
         .Where(mi => IsFactoryMethod(mi));
  }
}