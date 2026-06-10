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

      foreach (string key in this.As_StringDictionary.Keys)
      {
        if (!other.As_StringDictionary.ContainsKey(key)) return false;
        if (other.As_StringDictionary[key] != this.As_StringDictionary[key]) return false;
      }

      return true;
    }
  }
}