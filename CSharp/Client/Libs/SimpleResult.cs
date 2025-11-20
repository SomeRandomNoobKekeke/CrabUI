using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace BaroJunk
{
  /// <summary>
  /// Omg it's so fluent
  /// </summary>
  public class SimpleResult
  {
    static SimpleResult() => ProjectInfo.Add(new PackageInfo()
    {
      Name = "SimpleResult",
      Version = new Version(0, 0, 0)
      {
        Branch = "BaroJunk",
      }
    });

    public static SimpleResult Success(object result = null) => new SimpleResult()
    {
      Ok = true,
      Result = result,
    };
    public static SimpleResult Failure(string details = null, Exception ex = null) => new SimpleResult()
    {
      Ok = false,
      Details = details,
      Exception = ex,
    };

    public bool Ok;
    public string Details;
    public object Result;
    public Exception Exception;

    public override string ToString() => $"{(Ok ? $"Ok {Result?.GetType().Name}[{Result}]" : Details)}";
    public override bool Equals(object obj)
    {
      if (obj is not SimpleResult other) return false;
      return Ok == other.Ok && Object.Equals(Result, other.Result);
    }
  }


}