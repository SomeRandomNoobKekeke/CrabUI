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

    public override string ToString() => $"[{Start},{End}]";
    public static IntRange Parse(string raw)
    {
      if (raw == null || raw == "") return new IntRange(0, 0);

      string content = raw.Split('[', ']')[1];

      List<string> coords = content.Split(',').Select(s => s.Trim()).ToList();

      int start;
      int end;

      int.TryParse(coords.ElementAtOrDefault(0), out start);
      int.TryParse(coords.ElementAtOrDefault(1), out end);

      return new IntRange(start, end);
    }
  }

  /// <summary>
  /// Same as Range but with normal floats
  /// </summary>
  public struct FloatRange
  {
    public static FloatRange Zero = new FloatRange(0, 0);
    public float Start;
    public float End;
    public bool IsZero => Start == 0 && End == 0;
    public bool IsEmpty => End - Start <= 0;

    public float PosOf(float lambda) => (End - Start) * lambda;
    public float LambdaOf(float pos) => (pos - Start) / (End - Start);
    public FloatRange(float start, float end)
    {
      if (end >= start) (Start, End) = (start, end);
      else (End, Start) = (start, end);
    }
    public static bool operator ==(FloatRange a, FloatRange b) => a.Start == b.Start && a.End == b.End;
    public static bool operator !=(FloatRange a, FloatRange b) => a.Start != b.Start || a.End != b.End;

    public override string ToString() => $"[{Start},{End}]";

    public static FloatRange Parse(string raw)
    {
      if (raw == null || raw == "") return new FloatRange(0, 0);

      string content = raw.Split('[', ']')[1];

      List<string> coords = content.Split(',').Select(s => s.Trim()).ToList();

      float start;
      float end;

      float.TryParse(coords.ElementAtOrDefault(0), out start);
      float.TryParse(coords.ElementAtOrDefault(1), out end);

      return new FloatRange(start, end);
    }
  }
}