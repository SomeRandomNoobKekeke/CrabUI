using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;

namespace CursedUI
{

  /// <summary>
  /// Probably cringe and won't be used much
  /// </summary>
  public struct ElevatedProp<T>
  {
    public bool Elevated { get; set; }
    public T Value { get; set; }
  }
}