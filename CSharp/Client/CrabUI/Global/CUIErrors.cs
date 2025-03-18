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
  public partial class CUI
  {
    public static Dictionary<string, int> Errors = new();
    public static void Error(object msg, int maxPrints = 1, int tolerate = 5, bool silent = false)
    {
      string s = $"{msg}";
      if (!Errors.ContainsKey(s)) Errors[s] = 1;
      else Errors[s] = Errors[s] + 1;
      if (silent) return;
      if (tolerate < Errors[s] && Errors[s] <= maxPrints) Log($"CUI: {s} x{Errors[s]}", Color.Orange);
    }
  }
}
