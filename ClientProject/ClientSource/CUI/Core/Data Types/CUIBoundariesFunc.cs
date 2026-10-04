using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CursedUI
{

  /// <summary>
  /// Attempt to make serializable CUIBoundariesFunc
  /// Not flexible enough, not used
  /// </summary>
  public struct CUIBoundariesFunc : IParsable
  {
    public static CUIBoundariesFunc Free => new CUIBoundariesFunc();
    public static CUIBoundariesFunc Box => new CUIBoundariesFunc(0, 0, 0, 0);
    public static CUIBoundariesFunc HorizontalTube => new CUIBoundariesFunc(null, null, 0, 0);
    public static CUIBoundariesFunc VerticalTube => new CUIBoundariesFunc(0, 0, null, null);

    public float? MinX { get; set; }
    public float? MaxX { get; set; }
    public float? MinY { get; set; }
    public float? MaxY { get; set; }

    public CUIBoundaries Calculate(CUIRect rect)
    {
      return new CUIBoundaries(
        MinX.HasValue ? rect.Left + MinX.Value : null,
        MaxX.HasValue ? rect.Left + rect.Width + MaxX.Value : null,
        MinY.HasValue ? rect.Top + MinY.Value : null,
        MaxY.HasValue ? rect.Top + rect.Height + MaxY.Value : null
      );
    }


    public override string ToString() => $"[{MinX},{MaxX},{MinY},{MaxY}]";
    public static CUIBoundariesFunc Parse(string raw)
    {
      string content = raw.Split('[', ']')[1];

      var components = content.Split(',').Select(a => a.Trim());

      string minXPart = components.ElementAtOrDefault(0);
      string maxXPart = components.ElementAtOrDefault(1);
      string minYPart = components.ElementAtOrDefault(2);
      string maxYPart = components.ElementAtOrDefault(3);

      float? minX = null;
      float? maxX = null;
      float? minY = null;
      float? maxY = null;

      if (string.IsNullOrEmpty(minXPart)) minX = null;
      else minX = float.Parse(minXPart);

      if (string.IsNullOrEmpty(maxXPart)) maxX = null;
      else maxX = float.Parse(maxXPart);

      if (string.IsNullOrEmpty(minYPart)) minY = null;
      else minY = float.Parse(minYPart);

      if (string.IsNullOrEmpty(maxYPart)) maxY = null;
      else maxY = float.Parse(maxYPart);

      return new CUIBoundariesFunc(minX, maxX, minY, maxY);
    }

    static object IParsable.Parse(string raw) => Parse(raw);
    public string ToText() => ToString();

    public CUIBoundariesFunc(
      float? minX = null,
      float? maxX = null,
      float? minY = null,
      float? maxY = null
    )
    {
      MinX = minX;
      MaxX = maxX;
      MinY = minY;
      MaxY = maxY;
    }
  }
}