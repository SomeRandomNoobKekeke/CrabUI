using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using System.IO;
using System.Text;


namespace CUILibs
{
  public partial class Parser
  {
    public string Serialize(object o)
    {
      try
      {
        if (o is null) return Settings.NullTerm;
        if (o is string) return o as string;
        if (o is IParsable) return ((IParsable)o).ToText();
        if (ExtraSerializeMethods.ContainsKey(o.GetType())) return ExtraSerializeMethods[o.GetType()](o);
        if (o is IEnumerable) return SerializeEnumerable(o as IEnumerable);
      }
      catch (Exception e)
      {
        Logger.Warning($"Can't serialize [{o}]: {e.Message}");
      }

      try
      {
        return o.ToString();
      }
      catch (Exception e)
      {
        Logger.Warning($"Can't return [{o}].ToString(): {e.Message}");
        return $"{CreateDefaultFor(o.GetType())}";
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
        sb.Append(Settings.EnumerableSeparator);
        sb.Append(Serialize(enumerator.Current));
      }
      sb.Append("]");

      return sb.ToString();
    }

    public string SerializeDictionary(IDictionary dict)
    {
      return "wip";
    }
  }
}
