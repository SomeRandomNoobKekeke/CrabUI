using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;
namespace BaroJunk
{
  public class UTestDictResult : UTestResultBase
  {
    public record ValueDiff(object Key, object Value1, object Value2)
    {
      public override string ToString() => $"{Key} [{Value1}, {Value2}]";
    }

    //FEATURE
    // public static List<ValueDiff> Diff(UTestDictResult r1, UTestDictResult r2) {}


    private System.Collections.IDictionary Values;
    public override object Result { get => Values; set => Values = value as System.Collections.IDictionary; }

    public override bool Equals(object obj)
    {
      if (obj is not UTestDictResult other) return false;
      if (Values is not null && other.Values is not null)
      {
        foreach (System.Collections.DictionaryEntry kvp in Values)
        {
          if (!other.Values.Contains(kvp.Key)) return false;
          if (!Object.Equals(other.Values[kvp.Key], kvp.Value)) return false;
        }
        foreach (System.Collections.DictionaryEntry kvp in other.Values)
        {
          if (!Values.Contains(kvp.Key)) return false;
          if (!Object.Equals(Values[kvp.Key], kvp.Value)) return false;
        }
        return true;
      }
      return Values == other.Values;
    }

    public UTestDictResult(System.Collections.IDictionary values) => Values = values;

    public override string ToString()
    {
      StringBuilder sb = new("[\n");

      foreach (System.Collections.DictionaryEntry kvp in Values)
      {
        sb.Append($"    [{kvp.Key}] = [{kvp.Value}],\n");
      }
      sb.Remove(sb.Length - 1, 1);
      sb.Append("\n]");
      return sb.ToString();
    }
  }
}