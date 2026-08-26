using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using CUICodeGenerator;
using Microsoft.Xna.Framework;


namespace CursedUI
{
  public class Slot<T> // where T : class
  {
    private T _Value; public T Value
    {
      get => _Value;
      set
      {
        if (_Value is not null) TearDown?.Invoke(_Value);
        _Value = value;
        Changed?.Invoke(value);
        if (_Value is not null) WireUp?.Invoke(_Value);
      }
    }

    public event Action<T> Changed;

    public Action<T> OnWireUp { set { WireUp += value; } }
    public event Action<T> WireUp;

    public Action<T> OnTearDown { set { TearDown += value; } }
    public event Action<T> TearDown;
  }

  //Doesn't really work, part init order is random, if it's accessed from other init method there will be nre
  public class SlotPart<THost, T> : IPart
  {
    public THost Self { get; set; }
    public void Init() => Value = DefaultValue;
    public T DefaultValue { get; set; }
    private T _Value; public T Value
    {
      get => _Value;
      set
      {
        if (_Value is not null) TearDown?.Invoke(Self, _Value);
        _Value = value;
        if (_Value is not null) WireUp?.Invoke(Self, _Value);
      }
    }


    public Action<THost, T> OnWireUp { set { WireUp += value; } }
    public event Action<THost, T> WireUp;

    public Action<THost, T> OnTearDown { set { TearDown += value; } }
    public event Action<THost, T> TearDown;
  }
}