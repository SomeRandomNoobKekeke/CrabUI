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
    // public static T Parse(string raw);
    // public static string Serialize(T value);


    private HashSet<Type> AlreadyTriedRegisterParse = new();
    public Dictionary<Type, Func<string, object>> ExtraParsingMethods { get; } = new();

    public bool HasParse(Type T) => ExtraParsingMethods.ContainsKey(T);
    public void AddParse(Type T, Func<string, object> func) => ExtraParsingMethods[T] = func;
    public Func<string, object> GetParse(Type T) => ExtraParsingMethods.GetValueOrDefault(T);

    public bool ExtractParse(Type T)
    {
      AlreadyTriedRegisterParse.Add(T);

      MethodInfo parse = T.GetMethod(
        "Parse",
        BindingFlags.Public | BindingFlags.Static,
        new Type[] { typeof(string) }
      );

      if (parse == null) return false;

      try
      {
        AddParse(T, (string raw) => parse.Invoke(null, new object[] { raw }));
        return true;
      }
      catch (Exception e)
      {
        OnError.Raise(
          $"Couldn't register {T}.Parse in SimpleParser because [{e}|{e.InnerException}]"
        );
        return false;
      }
    }

    public void AddParse(Dictionary<Type, Func<string, object>> funcDict)
    {
      foreach (var (type, func) in funcDict)
      {
        AddParse(type, func);
      }
    }




    private HashSet<Type> AlreadyTriedRegisterSerialize = new();
    public Dictionary<Type, Func<object, string>> ExtraSerializingMethods { get; } = new();

    public bool HasSerialize(Type T) => ExtraSerializingMethods.ContainsKey(T);
    public void AddSerialize(Type T, Func<object, string> func) => ExtraSerializingMethods[T] = func;
    public Func<object, string> GetSerialize(Type T) => ExtraSerializingMethods.GetValueOrDefault(T);

    public bool ExtractSerialize(Type T)
    {
      AlreadyTriedRegisterSerialize.Add(T);

      MethodInfo serialize = T.GetMethod(
        "Serialize",
        BindingFlags.Public | BindingFlags.Static
      );

      if (serialize is null) return false;

      AddSerialize(T, (o) => (string)serialize.Invoke(null, new object[] { o }));
      return true;
    }

    public void AddSerialize(Dictionary<Type, Func<object, string>> funcDict)
    {
      foreach (var (type, func) in funcDict)
      {
        AddSerialize(type, func);
      }
    }

  }
}
