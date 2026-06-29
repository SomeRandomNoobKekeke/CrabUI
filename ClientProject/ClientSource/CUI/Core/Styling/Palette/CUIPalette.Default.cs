using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIPalette
  {
    /// <summary>
    /// I have no idea how many colors is required, my plan is this:
    /// I'll create new color for each use case
    /// If i see that some colors correlate i'll try to generalize them
    /// </summary>
    public static CUIPalette Default => new()
    {
      Colors = new()
      {
        ["button"] = new Color(255, 0, 0),
        ["text"] = new Color(255, 255, 255),
        ["frame"] = new Color(32, 0, 0),
        ["outercontrols"] = new Color(255, 255, 0),
        ["inputfocused"] = new Color(200, 0, 0),
        ["inputblured"] = new Color(64, 0, 0),
        ["inputselection"] = new Color(255, 255, 255),
        ["inputcaret"] = new Color(255, 255, 255),
        ["inputinvalid"] = new Color(255, 255, 0),
        ["border"] = new Color(150, 0, 0),
      }
    };
  }
}