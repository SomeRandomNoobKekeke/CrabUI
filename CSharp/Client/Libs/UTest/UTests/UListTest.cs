using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public class UListTest : UTest
  {
    public override void Adapt(object realValue, object expect)
    {
      Result = realValue switch
      {
        UTestResultBase => realValue as UTestResultBase,
        List<object> => new UTestListResult(realValue as List<object>),
        IEnumerable => new UTestListResult(new List<object>((realValue as IEnumerable).Cast<object>())),
        _ => throw new ArgumentException($"can't use [{realValue}] in UListTest, it's not assignable to List<object>")
      };

      Expected = expect switch
      {
        UTestResultBase => expect as UTestResultBase,
        List<object> => new UTestListResult(expect as List<object>),
        IEnumerable => new UTestListResult(new List<object>((expect as IEnumerable).Cast<object>())),
        _ => throw new ArgumentException($"can't expect [{expect}] in UListTest, it's not assignable to List<object>")
      };
    }

    public UListTest(object realValue, object expect, [CallerArgumentExpression("realValue")] string expression = "")
      : base(realValue, expect, expression) { }
    public UListTest(Func<object> method, object expect, [CallerArgumentExpression("method")] string expression = "")
      : base(method, expect, expression) { }
  }
}