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
    private HashSet<Type> AlreadyTriedRegisterParse = new();
    public Dictionary<Type, Func<string, object>> ExtraParsingMethods { get; } = new();

    public bool HasParsingMethod(Type T) => ExtraParsingMethods.ContainsKey(T);
    public void SetParsingMethod(Type T, Func<string, object> func) => ExtraParsingMethods[T] = func;
    public Func<string, object> GetParsingMethod(Type T) => ExtraParsingMethods.GetValueOrDefault(T);

    public bool TryRegisterParse(Type T)
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
        SetParsingMethod(T, (Func<string, object>)Delegate.CreateDelegate(typeof(Func<string, object>), parse));
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

    private HashSet<Type> AlreadyTriedRegisterSerialize = new();
    public Dictionary<Type, Func<object, string>> ExtraSerializingMethods { get; } = new();

    public bool HasSerializingMethod(Type T) => ExtraSerializingMethods.ContainsKey(T);
    public void SetSerializingMethod(Type T, Func<object, string> func) => ExtraSerializingMethods[T] = func;
    public Func<object, string> GetSerializingMethod(Type T) => ExtraSerializingMethods.GetValueOrDefault(T);

    public bool TryRegisterSerialize(Type T)
    {
      AlreadyTriedRegisterSerialize.Add(T);

      MethodInfo serialize = T.GetMethod(
        "Serialize",
        BindingFlags.Public | BindingFlags.Static
      );

      if (serialize is null) return false;

      SetSerializingMethod(T, (o) => (string)serialize.Invoke(null, new object[] { o }));
      return true;
    }
  }
}
