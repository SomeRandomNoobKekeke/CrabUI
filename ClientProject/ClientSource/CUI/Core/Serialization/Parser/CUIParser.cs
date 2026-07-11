using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUI
{
  public partial class CUIParser
  {
    public static string NullTerm = "{{null}}";
    public static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;

    public string Serialize(object o)
    {
      try
      {
        if (o is null) return NullTerm;
        if (o is IParsable) return ((IParsable)o).Serialize();
        if (ExtraSerializeMethods.ContainsKey(o.GetType())) return ExtraSerializeMethods[o.GetType()](o);
        return o.ToString();
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Can't serialize [{o}]: {e.Message}");
        return o.ToString();
      }
    }

    public T Parse<T>(string raw) => (T)Parse(raw, typeof(T));
    public object Parse(string raw, Type T)
    {
      try
      {
        if (raw == NullTerm || raw == null) return T.GetDefaultValue();

        if (T.IsAssignableTo(typeof(IParsable)))
        {
          MethodInfo? parse = T.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
          if (parse is null)
          {
            CUI.Logger.Warning($"Can't parse [{T}]");
            return T.GetDefaultValue();
          }

          return parse.Invoke(null, [raw]);
        }

        if (ExtraParseMethods.ContainsKey(T)) return ExtraParseMethods[T](raw);

        return ParseUnknown(raw, T);
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Can't parse [{raw}] into [{T}]: {e.Message}");
        return T.GetDefaultValue();
      }
    }

    private object ParseUnknown(string raw, Type T)
    {
      if (T == typeof(string)) return raw;

      if (T.IsPrimitive) return Convert.ChangeType(raw, T);
      if (T.IsEnum) return Enum.Parse(T, raw);
      return T.GetDefaultValue();
    }
  }
}
