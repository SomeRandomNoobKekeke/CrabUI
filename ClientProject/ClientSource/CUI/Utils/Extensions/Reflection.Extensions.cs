using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CursedUI
{
  public static class Reflection_Extensions
  {
    public static string GetFullName(this Type T)
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

    public static string GetFullMethodName(this MethodInfo mi)
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

    public static IEnumerable<Type> GetTypeChain(this Type T, Type rootType)
    {
      yield return T;

      while (T.BaseType != null && T.BaseType.IsAssignableTo(rootType))
      {
        T = T.BaseType;
        yield return T;
      }
    }
  }
}