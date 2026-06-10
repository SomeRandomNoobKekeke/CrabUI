using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class UTestListResult : UTestResultBase
  {
    private List<object> Values;
    public override object Result { get => Values; set => Values = value as List<object>; }

    public override bool Equals(object obj)
    {
      if (obj is not UTestListResult other) return false;
      if (Values is not null && other.Values is not null)
      {
        return Values.SequenceEqual(other.Values);
      }
      return Values == other.Values;
    }

    public UTestListResult(List<object> values) => Values = values;

    public override string ToString() => $"[\n    {String.Join(",\n    ", Values?.Select(o => o?.ToString()))}\n]";
  }
}