using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CrabUI
{
  public static class Utils
  {
    public static string SubstringSafe(this string s, int i)
      => s.Substring(0, Math.Clamp(i, 0, s.Length));

    public static string GetFullTypeName(this Type T)
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

    public static IEnumerable<Type> GetTypeChain(Type T, Type rootType)
    {
      yield return T;

      Type baseType = T.BaseType;
      while (baseType != null && baseType.IsAssignableTo(rootType))
      {
        yield return baseType;
        baseType = baseType.BaseType;
      }
    }
  }
}