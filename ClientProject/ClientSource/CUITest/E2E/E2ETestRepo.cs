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
  public class E2ETestRepo
  {
    public static bool IsE2ETest(Type T)
      => T.IsAssignableTo(typeof(IE2ETest));

    //Mb should be in some util class
    public static string GetFullTypeName(Type T)
    {
      List<string> parts = new List<string>() { T.Name };
      Type declaringType = T.DeclaringType;
      while (declaringType != null)
      {
        parts.Add(declaringType.Name);
        declaringType = declaringType.DeclaringType;
      }
      parts.Reverse();

      return string.Join('.', parts);
    }

    public Dictionary<string, Type> Tests { get; } = new();

    private void Add(Type T)
    {
      Tests[GetFullTypeName(T)] = T;
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