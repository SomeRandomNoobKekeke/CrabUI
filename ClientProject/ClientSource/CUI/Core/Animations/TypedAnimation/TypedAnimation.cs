using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class TypedAnimation<T> : AnimationCore
  {
    public T StartValue { get; set; }
    public T EndValue { get; set; }

    private Func<T, T, double, T> LerpFunc;

    public T Value => LerpFunc(StartValue, EndValue, Lambda);

    public Action<T> OnChanged { set { Changed += value; } }
    public event Action<T> Changed;


    public TypedAnimation()
    {
      LerpFunc = ValueLerpFuncs.Get<T>();
      Updated += (l) =>
      {
        Changed?.Invoke(Value);
      };
    }
  }
}