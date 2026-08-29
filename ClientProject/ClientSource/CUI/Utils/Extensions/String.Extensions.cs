using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CursedUI
{
  public static class String_Extensions
  {
    public static string SubstringSafe(this string s, int i)
      => s.Substring(0, Math.Clamp(i, 0, s.Length));
  }
}