using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public class CUIReactiveProp<T> : CUIAwareProp<T>
  {
    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        ValueSet?.Invoke(Value);
      }
    }

    public Action<T> DoOnValueSet { set => ValueSet += value; }
    public event Action<T> ValueSet;
  }
}