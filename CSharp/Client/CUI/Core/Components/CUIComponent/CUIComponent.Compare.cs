using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public bool IsEqualTo(CUIComponent other)
    {
      if (GetType() != other.GetType()) return false;

      foreach (string key in this.As_Dictionary.Keys)
      {
        if (!other.As_Dictionary.ContainsKey(key)) return false;
        if (other.As_Dictionary[key] != this.As_Dictionary[key]) return false;
      }

      return true;
    }
  }
}