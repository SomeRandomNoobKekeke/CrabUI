using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class UTestError : UTestResultBase
  {
    public Exception Error;
    public override object Result
    {
      get => Error;
      set => Error = value as Exception;
    }

    public override bool Equals(object obj)
    {
      if (obj is not UTestError other) return false;
      if (Error is not null && other.Error is not null)
      {
        return Error.GetType() == other.Error.GetType();
      }
      return Error == other.Error;
    }

    public UTestError(Exception ex) => Error = ex;

    public override string ToString() => $"{Error.GetType().Name}('{Error.Message}')";
  }
}