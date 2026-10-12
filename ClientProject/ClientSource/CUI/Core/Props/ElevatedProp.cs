using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;

namespace CursedUI
{
  public struct ElevatedProp<T>
  {
    public bool WasModified { get; private set; }

    private T _Value; public T Value
    {
      get => _Value;
      set
      {
        _Value = value;
        WasModified = true;
      }
    }
  }
}