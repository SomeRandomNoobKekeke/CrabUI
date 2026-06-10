using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public class UThrowTest : UTest
  {
    public override void Adapt(object realValue, object expect)
    {
      Result = realValue switch
      {
        UTestResultBase => realValue as UTestResultBase,
        _ => new UTestResult(realValue),
      };

      Expected = expect switch
      {
        Exception ex => new UTestError(ex),
        UTestResultBase => expect as UTestResultBase,
        _ => new UTestResult(expect),
      };
    }

    public UThrowTest(Func<object> method, object expect, [CallerArgumentExpression("method")] string expression = "")
      : base(method, expect, expression) { }

    public UThrowTest(Action method, object expect, [CallerArgumentExpression("method")] string expression = "")
      : base(() => { method(); return "ok"; }, expect, expression) { }
  }
}