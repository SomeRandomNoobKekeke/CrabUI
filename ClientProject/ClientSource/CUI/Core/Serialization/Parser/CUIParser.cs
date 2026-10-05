using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;
using CUILibs;

namespace CursedUI
{
  public partial class CUIParser
  {
    public static char EnumerableSeparator = ';';
    public static string NullTerm = "{{null}}";
    public static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;
    public static object CreateDefaultFor(Type T)
    {
      if (T.IsValueType) return Activator.CreateInstance(T);
      return null;
    }

    public string Serialize(object o)
    {
      try
      {
        if (o is null) return NullTerm;
        if (o is string) return o as string;
        if (o is IParsable) return ((IParsable)o).ToText();
        if (ExtraSerializeMethods.ContainsKey(o.GetType())) return ExtraSerializeMethods[o.GetType()](o);
        if (o is IEnumerable) return SerializeEnumerable(o as IEnumerable);
        return o.ToString();
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Can't serialize [{o}]: {e.Message}");
        return o.ToString();
      }
    }

    /// <summary>
    /// Basically $"[{string.Join(';', items)}]"
    /// Had to itterate manually because it's mot generic IEnumerable
    /// </summary>
    public string SerializeEnumerable(IEnumerable items)
    {
      StringBuilder sb = new StringBuilder("[");

      IEnumerator enumerator = items.GetEnumerator();

      if (!enumerator.MoveNext()) return "[]";
      sb.Append(Serialize(enumerator.Current));

      while (enumerator.MoveNext())
      {
        sb.Append(EnumerableSeparator);
        sb.Append(Serialize(enumerator.Current));
      }
      sb.Append("]");

      return sb.ToString();
    }

    public T Parse<T>(string raw) => (T)Parse(raw, typeof(T));
    public object Parse(string raw, Type T)
    {
      try
      {
        if (raw == NullTerm || raw == null) return T.GetDefaultValue();

        if (T.IsAssignableTo(typeof(IParsable)))
        {
          MethodInfo parse = T.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
          if (parse is null)
          {
            CUI.Logger.Warning($"Can't parse [{raw}] into [{T}]: It doesn't have Parse method");
            return CreateDefaultFor(T);
          }
          return parse.Invoke(null, [raw]);
        }

        if (ExtraParseMethods.ContainsKey(T)) return ExtraParseMethods[T](raw);

        //TODO if (o is IEnumerable) ??

        return ParseUnknown(raw, T);
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Can't parse [{raw}] into [{T}]: {Logger.Wrap.ExceptionMessage(e)}");
        return T.GetDefaultValue();
      }
    }

    private object ParseUnknown(string raw, Type T)
    {
      if (T == typeof(string)) return raw;

      if (T.IsPrimitive) return Convert.ChangeType(raw, T);
      if (T.IsEnum) return Enum.Parse(T, raw);
      if (IsNullable(T)) return ParseUnknown(raw, Nullable.GetUnderlyingType(T));
      return T.GetDefaultValue();
    }
  }
}
