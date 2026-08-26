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

namespace CursedUI
{
  public partial class CUIParser
  {

    public string SerializeArray(params object[] args)
      => $"[{String.Join(',', args.Select(Serialize))}]";

    public T[] ParseArray<T>(string raw)
    {
      if (raw == null) return [];

      string content = raw.Trim(' ', '[', ']');
      if (content == "") return [];

      return content.Split(',').Select(part => (T)Parse(part, typeof(T))).ToArray();
    }

    public string SerializeDict(Dictionary<string, string> dict)
      => $"[{String.Join(',', dict.Select(kvp => $"{kvp.Key}:{Serialize(kvp.Value)}"))}]";

    // public Dictionary<string, string> ParseDict(string raw)
    // {

    // }
  }
}
