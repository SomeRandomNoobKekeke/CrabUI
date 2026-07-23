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
using System.IO;

namespace CrabUIUser
{
  public class E2ETestRepo
  {
    public static bool IsE2ETest(Type T)
      => T.IsAssignableTo(typeof(IE2ETest));

    public Dictionary<string, Type> Tests { get; } = new();

    private void Add(Type T)
    {
      Tests[T.GetFullName()] = T;
    }
    public void AddPack(Type testPack)
    {
      foreach (Type nested in testPack.GetNestedTypes())
      {
        if (IsE2ETest(nested)) Add(nested);
      }
    }
  }
}