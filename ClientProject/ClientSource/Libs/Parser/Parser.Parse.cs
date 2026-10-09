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
    private EnumerableParser enumerableParser = new();
    private DictionaryParser dictionaryParser = new();

    public T Parse<T>(string raw) => (T)Parse(raw, typeof(T));
    public object Parse(string raw, Type T)
    {
      try
      {
        if (raw == Settings.NullTerm || raw == null) return T.GetDefaultValue();

        if (T == typeof(string)) return raw;

        if (T.IsAssignableTo(typeof(IParsable)))
        {
          MethodInfo parse = T.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
          if (parse is null)
          {
            Logger.Warning($"Can't parse [{raw}] into [{T}]: It doesn't have Parse method");
            return CreateDefaultFor(T);
          }
          return parse.Invoke(null, [raw]);
        }

        if (ExtraParseMethods.ContainsKey(T)) return ExtraParseMethods[T](raw);

        if (T.IsAssignableTo(typeof(IEnumerable<string>))) return SplitEnumerable(raw);

        return ParseValueType(raw, T);
      }
      catch (Exception e)
      {
        Logger.Warning($"Can't parse [{raw}] into [{T}]: {e.InnerException?.Message ?? e.Message}");
        return T.GetDefaultValue();
      }
    }

    private object ParseValueType(string raw, Type T)
    {
      if (T.IsPrimitive) return Convert.ChangeType(raw, T);
      if (T.IsEnum) return Enum.Parse(T, raw);
      if (IsNullable(T)) return ParseValueType(raw, Nullable.GetUnderlyingType(T));
      return T.GetDefaultValue();
    }

    public List<string> SplitEnumerable(string raw) => enumerableParser.Split(raw);
    public IEnumerable<T> ParseEnumerable<T>(string raw)
      => enumerableParser.Split(raw).Select(Parse<T>);

    public Dictionary<string, string> SplitDict(string raw) => dictionaryParser.Split(raw);

  }
}
