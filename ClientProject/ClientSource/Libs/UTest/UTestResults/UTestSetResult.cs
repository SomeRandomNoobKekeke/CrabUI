using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class UTestSetResult : UTestResultBase
  {
    private HashSet<object> Values;
    public override object Result { get => Values; set => Values = value as HashSet<object>; }

    public override bool Equals(object obj)
    {
      if (obj is not UTestSetResult other) return false;
      if (Values is not null && other.Values is not null)
      {
        return Values.SetEquals(other.Values);
      }
      return Values == other.Values;
    }

    public UTestSetResult(HashSet<object> values) => Values = values;

    public override string ToString() => $"[{String.Join(", ", Values?.Select(o => o?.ToString()))}]";
  }
}