using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public partial class TypedAnimation<T>
  {
    private AnimationCore Core { get; } = new();

    public T StartValue { get; set; }
    public T EndValue { get; set; }

    private Func<T, T, double, T> LerpFunc;

    public T Value => LerpFunc(StartValue, EndValue, Core.Lambda);
    public event Action<T> Changed;


    public TypedAnimation()
    {
      LerpFunc = (Func<T, T, double, T>)ValueLerpFuncs.Mapping[typeof(T)];
      Core.Updated += (l) => Changed?.Invoke(Value);
    }
  }
}