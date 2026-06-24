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
using System.IO;

namespace CrabUIUser
{
  public class SnapshotTestRepo
  {
    public static bool IsSnapshotTestFunc(MethodInfo mi)
          => mi.ReturnType.IsAssignableTo(typeof(CUIComponent)) && mi.GetParameters().Length == 0;



    public Dictionary<string, SnapshotTest> Tests { get; } = new();
    public Dictionary<string, Dictionary<string, SnapshotTest>> GroupedTests { get; } = new();

    public ICollection<string> Groups => GroupedTests.Keys;

    public void Add(SnapshotTest test)
    {
      Tests[test.Name] = test;

      if (!GroupedTests.ContainsKey(test.Group)) GroupedTests[test.Group] = new();
      GroupedTests[test.Group][test.Name] = test;
    }
    public void Add(MethodInfo mi) => Add(SnapshotTest.FromMethodInfo(mi));

    public void Add(Type testPack)
    {
      foreach (MethodInfo mi in testPack.GetMethods(BindingFlags.Public | BindingFlags.Static))
      {
        if (IsSnapshotTestFunc(mi)) Add(mi);
      }

      foreach (Type nested in testPack.GetNestedTypes())
      {
        Add(nested);
      }
    }
  }
}