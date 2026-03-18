using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;

namespace CrabUI
{
  public class CUIReactiveProp<T> : CUIProp<T>
  {
    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        ValueSet?.Invoke(value);
      }
    }

    public Action<T> OnValueSet { set => ValueSet += value; }
    public event Action<T> ValueSet;
  }
}