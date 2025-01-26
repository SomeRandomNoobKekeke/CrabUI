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
  /// Same as Range but with normal ints
  /// </summary>
  public struct IntRange
  {
    public static IntRange Zero = new IntRange(0, 0);
    public int Start;
    public int End;
    public bool IsZero => Start == 0 && End == 0;
    public bool IsEmpty => End - Start <= 0;
    public IntRange(int start, int end)
    {
      if (end >= start) (Start, End) = (start, end);
      else (End, Start) = (start, end);
    }
    public static bool operator ==(IntRange a, IntRange b) => a.Start == b.Start && a.End == b.End;
    public static bool operator !=(IntRange a, IntRange b) => a.Start != b.Start || a.End != b.End;
  }
}