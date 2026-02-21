using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIReactiveProp<T> : CUIProp<T>
  {
    public object Host { get; set; }
    public string Name { get; set; }


    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        ValueSet?.Invoke(value);
      }
    }

    protected void RaiseValueSet(T value) => ValueSet?.Invoke(value);
    public Action<T> OnValueSet { set => ValueSet += value; }
    public event Action<T> ValueSet;
  }
}