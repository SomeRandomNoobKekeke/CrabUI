using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public class UDictTest : UTest
  {
    public override void Adapt(object realValue, object expect)
    {
      Result = realValue switch
      {
        UTestResultBase => realValue as UTestResultBase,
        System.Collections.IDictionary => new UTestDictResult(realValue as System.Collections.IDictionary),
        _ => throw new ArgumentException($"can't use [{realValue}] in UDictTest, it's not IDictionary")
      };

      Expected = expect switch
      {
        UTestResultBase => expect as UTestResultBase,
        System.Collections.IDictionary => new UTestDictResult(expect as System.Collections.IDictionary),
        _ => throw new ArgumentException($"can't use [{expect}] in UDictTest, it's not IDictionary")
      };
    }

    public UDictTest(object realValue, object expect, [CallerArgumentExpression("realValue")] string expression = "")
      : base(realValue, expect, expression) { }
    public UDictTest(Func<object> method, object expect, [CallerArgumentExpression("method")] string expression = "")
      : base(method, expect, expression) { }
  }
}