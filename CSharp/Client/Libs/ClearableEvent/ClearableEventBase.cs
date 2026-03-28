using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public abstract class ClearableEventBase
  {
    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public ClearableEventBase()
    {
      ID = MaxID++;
    }

    public override int GetHashCode() => ID;

    //TODO do i even need this?
    // public override bool Equals(object obj)
    // {
    //   //TODO test, i'm sure it's not supposed to work like this
    //   if (obj is not ClearableEventBase other) return false;
    //   return other.ID == ID;
    // }
  }
}