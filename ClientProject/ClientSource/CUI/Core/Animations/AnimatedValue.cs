using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CursedUI
{
  //LINK:/ClientProject\Tests\CUITest\Snapshots\Tests\Random\AnimatedValue.cs
  public class AnimatedValue<T>
  {
    private TypedAnimation<T> _Animation; public TypedAnimation<T> Animation
    {
      get
      {
        if (_Animation is null)
        {
          _Animation = new TypedAnimation<T>()
          {
            Func = AnimationFunctions.EasyOut,
            Duration = 0,
          };

          Animation.Changed += (v) => Value = v;
        }

        return _Animation;
      }
    }

    public Action<T> OnChanged { set { Changed += value; } }
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
        _Target = value;

        if (Animation is null || Animation.Duration == 0)
        {
          Value = value;
        }
        else
        {
          Animation.StartValue = Value;
          Animation.EndValue = _Target;
          Animation.RunFromStart();
        }
      }
    }

    public AnimatedValue() { }
    public AnimatedValue(T value) : this()
    {
      _Value = value;
    }
  }


}