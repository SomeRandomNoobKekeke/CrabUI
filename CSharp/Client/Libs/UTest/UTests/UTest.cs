using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public partial class UTest
  {
    /// <summary>
    /// Actually just annoying feature, if expression is unreadable you should set it manually
    /// </summary>
    public static int TooLongForAName = 300;
    public static string VeryLongExpressionWarning = "Very long expression";
    public UTestResultBase Expected;
    public UTestResultBase Result;
    public string Name = "Unnamed";
    public bool Passed => Object.Equals(Result, Expected);
    public string DetailsOnFail;

    public virtual void Adapt(object realValue, object expect)
    {
      Result = realValue switch
      {
        UTestResultBase => realValue as UTestResultBase,
        _ => new UTestResult(realValue),
      };

      Expected = expect switch
      {
        UTestResultBase => expect as UTestResultBase,
        _ => new UTestResult(expect),
      };
    }

    public UTest(object realValue, object expect, [CallerArgumentExpression("realValue")] string expression = "")
      => Init(realValue, expect, expression);
    public UTest(Func<object> method, object expect, [CallerArgumentExpression("method")] string expression = "")
    {
      ArgumentNullException.ThrowIfNull(method);

      try
      {
        Init(method.Invoke(), expect, expression);
      }
      catch (Exception e)
      {
        Init(new UTestError(e), expect, expression);
      }
    }

    private void Init(object realValue, object expect, string expression)
    {
      Name = expression is null ? null : expression.Length > TooLongForAName ? VeryLongExpressionWarning : expression;
      Adapt(realValue, expect);
    }

    public override string ToString()
    {
      if (Passed)
      {
        return $"[{UTestLogger.AsText(Name)}] Passed | match: [{UTestLogger.AsText(Result)}]";
      }
      else
      {
        return $"[{UTestLogger.AsText(Name)}] Failed | expected: [{UTestLogger.AsText(Expected)}] got: [{UTestLogger.AsText(Result)}]{(DetailsOnFail is not null ? $"({DetailsOnFail})" : "")}";
      }

    }

    public void Log() => UTestLogger.LogTest(this);
  }
}