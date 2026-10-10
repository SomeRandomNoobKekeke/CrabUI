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
        if (o is IDictionary) return SerializeDictionary(o as IDictionary);
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
    /// Basically $"[{string.Join(',', items)}]"
    /// Had to itterate manually because it's not generic IEnumerable
    /// </summary>
    public string SerializeEnumerable(IEnumerable items)
    {
      //Wrap in `` if it's sus
      string SerializeItem(object item)
      {
        if (item.GetType().IsPrimitive) return Serialize(item);
        // if (item.GetType().IsAssignableTo(typeof(IEnumerable))) return Serialize(item);
        // if (item.GetType().IsAssignableTo(typeof(IDictionary))) return Serialize(item);

        return $"{Settings.WhenSerializing.OpeningQuote}{Serialize(item)}{Settings.WhenSerializing.ClosingQuote}";
      }

      StringBuilder sb = new StringBuilder(Settings.WhenSerializing.EnumerableStart);

      IEnumerator enumerator = items.GetEnumerator();

      if (!enumerator.MoveNext()) // It's empty
      {
        sb.Append(Settings.WhenSerializing.EnumerableEnd);
        return sb.ToString();
      }

      // Serialize 1 item without EnumerableSeparator
      sb.Append(SerializeItem(enumerator.Current));

      while (enumerator.MoveNext())
      {
        sb.Append(Settings.WhenSerializing.EnumerableSeparator);
        sb.Append(SerializeItem(enumerator.Current));
      }
      sb.Append(Settings.WhenSerializing.EnumerableEnd);

      return sb.ToString();
    }

    /// <summary>
    /// {asd:123,qwe:321}
    /// </summary>
    public string SerializeDictionary(IDictionary dict)
    {
      string SerializeKey(object key) => Serialize(key);

      //Wrap in `` if it's sus
      string SerializeValue(object value)
      {
        if (value.GetType().IsPrimitive) return Serialize(value);
        // if (value.GetType().IsAssignableTo(typeof(IEnumerable))) return Serialize(value);
        // if (value.GetType().IsAssignableTo(typeof(IDictionary))) return Serialize(value);

        return $"{Settings.WhenSerializing.OpeningQuote}{Serialize(value)}{Settings.WhenSerializing.ClosingQuote}";
      }

      StringBuilder sb = new StringBuilder();
      sb.Append(Settings.WhenSerializing.DictStart);

      IEnumerator EKey = dict.Keys.GetEnumerator();

      if (!EKey.MoveNext()) // It's empty
      {
        sb.Append(Settings.WhenSerializing.DictEnd);
        return sb.ToString();
      }

      // Serialize 1 kvp without DictSeparator
      sb.Append(SerializeKey(EKey.Current));
      sb.Append(Settings.WhenSerializing.DictKVSeparator);
      sb.Append(SerializeValue(dict[EKey.Current]!));

      while (EKey.MoveNext())
      {
        sb.Append(Settings.WhenSerializing.DictSeparator);

        sb.Append(SerializeKey(EKey.Current));
        sb.Append(Settings.WhenSerializing.DictKVSeparator);
        sb.Append(SerializeValue(dict[EKey.Current]!));
      }
      sb.Append(Settings.WhenSerializing.DictEnd);

      return sb.ToString();
    }

  }
}
