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

    //Mb should be in some util class
    public static string GetFullMethodName(MethodInfo mi)
    {
      List<string> parts = new List<string>() { mi.Name };
      Type declaringType = mi.DeclaringType;
      while (declaringType != null)
      {
        parts.Add(declaringType.Name);
        declaringType = declaringType.DeclaringType;
      }
      parts.Reverse();

      return string.Join('.', parts);
    }

    public Dictionary<string, SnapshotTest> Tests { get; } = new();


    public void Add(SnapshotTest test) => Tests[test.Name] = test;
    public void Add(Func<CUIComponent> TestFunc, string Name) => Add(new SnapshotTest(TestFunc, Name));
    public void Add(MethodInfo mi) => Add(
      (Func<CUIComponent>)Delegate.CreateDelegate(typeof(Func<CUIComponent>), mi),
      GetFullMethodName(mi)
    );

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