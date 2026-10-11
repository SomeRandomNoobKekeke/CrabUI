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
        if (Settings.ExtraSerializeMethods.ContainsKey(o.GetType()))
        {
          return Settings.ExtraSerializeMethods[o.GetType()](o);
        }
        if (o is IParsable) return ((IParsable)o).ToText();
        if (o is IDictionary) return SerializeDict(o as IDictionary);
        if (o is IEnumerable) return SerializeList(o as IEnumerable);
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


    private bool ShouldWrapInQuotes(object item)
    {
      if (item is string) return true;
      if (item is IParsable parsable) return parsable.WrapInQuotes;
      if (item.GetType().IsPrimitive) return false;
      if (item.GetType().IsAssignableTo(typeof(IEnumerable))) return false;
      if (item.GetType().IsAssignableTo(typeof(IDictionary))) return false;
      return true;
    }

    private string WrapIfNeeded(object item)
    {
      if (ShouldWrapInQuotes(item))
      {
        return $"{Settings.WhenSerializing.OpeningQuote}{Serialize(item)}{Settings.WhenSerializing.ClosingQuote}";
      }

      return Serialize(item);
    }


    /// <summary>
    /// Basically $"[{string.Join(',', items)}]"
    /// Had to itterate manually because it's not generic IEnumerable
    /// </summary>
    public string SerializeList(IEnumerable items)
    {
      StringBuilder sb = new StringBuilder(Settings.WhenSerializing.ListStart);

      IEnumerator enumerator = items.GetEnumerator();

      if (!enumerator.MoveNext()) // It's empty
      {
        sb.Append(Settings.WhenSerializing.ListEnd);
        return sb.ToString();
      }

      // Serialize 1 item without EnumerableSeparator
      sb.Append(WrapIfNeeded(enumerator.Current));

      while (enumerator.MoveNext())
      {
        sb.Append(Settings.WhenSerializing.ListSeparator);
        sb.Append(WrapIfNeeded(enumerator.Current));
      }
      sb.Append(Settings.WhenSerializing.ListEnd);

      return sb.ToString();
    }

    /// <summary>
    /// {asd:123,qwe:321}
    /// </summary>
    public string SerializeDict(IDictionary dict)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(Settings.WhenSerializing.DictStart);

      IEnumerator EKey = dict.Keys.GetEnumerator();

      if (!EKey.MoveNext()) // It's empty
      {
        sb.Append(Settings.WhenSerializing.DictEnd);
        return sb.ToString();
      }

      // Serialize 1 kvp without DictSeparator
      sb.Append(Serialize(EKey.Current));
      sb.Append(Settings.WhenSerializing.DictKVSeparator);
      sb.Append(WrapIfNeeded(dict[EKey.Current]!));

      while (EKey.MoveNext())
      {
        sb.Append(Settings.WhenSerializing.DictSeparator);

        sb.Append(Serialize(EKey.Current));
        sb.Append(Settings.WhenSerializing.DictKVSeparator);
        sb.Append(WrapIfNeeded(dict[EKey.Current]!));
      }
      sb.Append(Settings.WhenSerializing.DictEnd);

      return sb.ToString();
    }

  }
}
