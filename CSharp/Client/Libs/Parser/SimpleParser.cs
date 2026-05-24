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

namespace BaroJunk
{
  public partial class SimpleParser
  {
    public static string NullTerm = "{{null}}";
    public static object DefaultFor(Type T)
    {
      if (T == typeof(string)) return null;
      if (T.IsValueType) return Activator.CreateInstance(T);
      return null;
    }

    static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;


    public ClearableEvent<string> OnError { get; } = new();

    public T Parse<T>(string raw) => (T)Parse(raw, typeof(T));
    public object Parse(string raw, Type T)
    {
      if (raw == null) return null;
      if (T == typeof(string)) return raw;

      if (T.IsPrimitive) return ParsePrimitive(raw, T);
      if (T.IsEnum) return ParseEnum(raw, T);
      if (IsNullable(T)) return ParseNullable(raw, T);
      if (!T.IsPrimitive) return ParseComplex(raw, T);

      return DefaultFor(T);
    }

    private object ParsePrimitive(string raw, Type T)
    {
      MethodInfo parse = T.GetMethod(
        "Parse",
        BindingFlags.Public | BindingFlags.Static,
        new Type[] { typeof(string) }
      );

      try
      {
        return parse.Invoke(null, new object[] { raw });
      }
      catch (Exception e)
      {
        OnError.Raise(
          $"Couldn't parse [{raw}] into primitive type [{T}] because [{e}|{e.InnerException}]"
        );
      }

      return DefaultFor(T);
    }

    private object ParseEnum(string raw, Type T)
    {
      try
      {
        return Enum.Parse(T, raw);
      }
      catch (Exception e)
      {
        OnError.Raise(
          $"Couldn't parse [{raw}] into Enum [{T}] because [{e}|{e.InnerException}]"
        );
      }

      return DefaultFor(T);
    }

    private object ParseNullable(string raw, Type T)
    {
      if (raw == NullTerm) return null;
      return Parse(raw, Nullable.GetUnderlyingType(T));
    }

    private object ParseComplex(string raw, Type T)
    {
      if (!HasParse(T))
      {
        if (AlreadyTriedRegisterParse.Contains(T))
        {
          return null;
        }

        if (!ExtractParse(T))
        {
          OnError.Raise($"[{T}] doesn't have a parse method");
          return null;
        }
      }

      try
      {
        return GetParse(T).Invoke(raw);
      }
      catch (Exception e)
      {
        OnError.Raise($"Couldn't parse [{raw}] into [{T}], Custom parsing method threw: [{e}|{e.InnerException}]");
        return null;
      }
    }

    // There's no way to Serialize null correctly
    public string Serialize(object o) => Serialize(o, o.GetType());
    public string Serialize(object o, Type T)
    {
      if (T == typeof(string)) return (string)o;
      if (T.IsPrimitive) return o.ToString();
      if (T.IsEnum) return o.ToString();
      if (IsNullable(T))
      {
        if (o is null) return NullTerm;
        return Serialize(o, Nullable.GetUnderlyingType(T));
      }

      if (!HasSerialize(T))
      {
        if (AlreadyTriedRegisterSerialize.Contains(T))
        {
          return o.ToString();
        }

        if (!ExtractSerialize(T))
        {
          OnError.Raise($"[{T}] doesn't have a Serialize method");
          return o.ToString();
        }
      }

      try
      {
        return GetSerialize(T).Invoke(o);
      }
      catch (Exception e)
      {
        OnError.Raise($"Couldn't serialize [{T}], Custom serialize method threw: [{e}|{e.InnerException}]");
        return o.ToString();
      }
    }



    public SimpleParser()
    {
      AddParse(SimpleParserDefaultMethods.Parse);
      AddSerialize(SimpleParserDefaultMethods.Serialize);
    }
  }
}
