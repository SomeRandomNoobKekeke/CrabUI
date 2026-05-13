using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public class CUIProp<T> : IAware
  {
    public object HostComponent { get; set; }
    public string HostPropName { get; set; }

    protected T _value;
    public virtual T Value
    {
      get => _value;
      set => _value = value;
    }

    public T DefaultValue { set { _value = value; } }
  }
}