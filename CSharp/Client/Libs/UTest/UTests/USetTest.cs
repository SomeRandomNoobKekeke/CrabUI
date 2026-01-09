using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public class USetTest : UTest
  {
    public override void Adapt(object realValue, object expect)
    {
      Result = realValue switch
      {
        UTestResultBase => realValue as UTestResultBase,
        HashSet<object> => new UTestSetResult(realValue as HashSet<object>),
        IEnumerable => new UTestSetResult(new HashSet<object>((realValue as IEnumerable).Cast<object>())),
        _ => throw new ArgumentException($"can't use [{realValue}] in USetTest, it's not assignable to HashSet<object>")
      };

      Expected = expect switch
      {
        UTestResultBase => expect as UTestResultBase,
        HashSet<object> => new UTestSetResult(expect as HashSet<object>),
        IEnumerable => new UTestSetResult(new HashSet<object>((expect as IEnumerable).Cast<object>())),
        _ => throw new ArgumentException($"can't expect [{expect}] in USetTest, it's not assignable to HashSet<object>")
      };
    }

    public USetTest(object realValue, object expect, [CallerArgumentExpression("realValue")] string expression = "")
      : base(realValue, expect, expression) { }
    public USetTest(Func<object> method, object expect, [CallerArgumentExpression("method")] string expression = "")
      : base(method, expect, expression) { }
  }
}