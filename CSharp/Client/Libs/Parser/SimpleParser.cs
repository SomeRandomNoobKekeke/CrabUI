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
    public static object DefaultFor(Type T)
    {
      if (T == typeof(string)) return null;
      return Activator.CreateInstance(T);
    }

    public ClearableEvent<string> OnError { get; } = new();



    /// <summary>
    /// Null is serialized into this, so you could distinguish null and empty string
    /// </summary>
    public string NullTerm = "{{null}}";

    public T Parse<T>(string raw) => (T)Parse(raw, typeof(T));
    public object Parse(string raw, Type T)
    {
      if (raw == null || raw == NullTerm) return null;
      if (T == typeof(string)) return raw;

      if (T.IsPrimitive) return ParsePrimitive(raw, T);
      if (T.IsEnum) return ParseEnum(raw, T);
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

    public string Serialize(object o)
    {
      if (o is null) return NullTerm;
      Type T = o.GetType();

      if (T == typeof(string)) return (string)o;
      if (T.IsPrimitive) return o.ToString();

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
