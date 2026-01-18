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
  /// <summary>
  /// Just a wrapper around prop
  /// </summary>
  public class CUIProp<T>
  {
    /// <summary>
    /// Object that contains the prop
    /// </summary>
    public object Host { get; set; }

    /// <summary>
    /// Prop name in containing object
    /// </summary>
    public string Name { get; set; }

    protected T _value;
    public virtual T Value
    {
      get => _value;
      set => _value = value;
    }
  }
}