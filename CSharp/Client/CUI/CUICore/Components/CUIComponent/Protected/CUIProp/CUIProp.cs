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
  public class CUIProp<T>
  {
    protected T _value;
    public virtual T Value
    {
      get => _value;
      set => _value = value;
    }
  }
}