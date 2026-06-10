using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class UTestResult : UTestResultBase
  {
    private object result;
    public override object Result { get => result; set => result = value; }

    public override bool Equals(object obj)
    {
      if (obj is not UTestResult other) return false;
      return Object.Equals(Result, other.Result);
    }

    public UTestResult(object result) => Result = result;

    public override string ToString() => Result?.ToString();
  }
}