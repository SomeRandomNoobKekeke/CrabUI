using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CursedUI
{
  //LINK:/ClientProject\InMemory\CUITest\Snapshots\Tests\Random\AnimatedValue.cs
  public class AnimatedValue<T>
  {
    public TypedAnimation<T> Animation { get; } = new()
    {
      // Duration = 1,
    };

    public event Action<T> Changed;

    private T _Value; public T Value
    {
      get => _Value;
      set
      {
        _Value = value;
        Changed?.Invoke(value);
      }
    }

    private T _Target; public T Target
    {
      get => _Target;
      set
      {
        if (Equals(Value, value)) return;

        _Target = value;

        Animation.StartValue = Value;
        Animation.EndValue = _Target;
        Animation.RunFromStart();
      }
    }

    public AnimatedValue()
    {
      Animation.Changed += (v) => Value = v;
      Animation.Func = AnimationFunctions.EasyOut;
    }

    public AnimatedValue(T value) : this()
    {
      _Value = value;
    }
  }


}