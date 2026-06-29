using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Vector2 but with bools
  /// </summary>
  public struct CUIBool2
  {
    public bool X;
    public bool Y;

    public CUIBool2(bool x = false, bool y = false)
    {
      X = x;
      Y = y;
    }

    public override string ToString() => $"[{X},{Y}]";
    public static CUIBool2 Parse(string s)
    {
      string content = s.Substring(
        s.IndexOf('[') + 1,
        s.IndexOf(']') - s.IndexOf('[') - 1
      );

      var components = content.Split(',').Select(a => a.Trim());

      string sx = components.ElementAtOrDefault(0);
      string sy = components.ElementAtOrDefault(1);

      bool x = false;
      bool y = false;

      if (sx != null && sx != "") x = bool.Parse(sx);
      if (sy != null && sy != "") y = bool.Parse(sy);

      return new CUIBool2(x, y);
    }
  }
}