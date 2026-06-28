using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIPalette
  {
    public static CUIPalette Default => new()
    {
      Colors = new()
      {
        ["border"] = new Color(200, 0, 0),
        ["panel1"] = new Color(180, 0, 0),
        ["panel2"] = new Color(150, 0, 0),
        ["panel3"] = new Color(128, 0, 0),
        ["panel4"] = new Color(80, 0, 0),
        ["activeinput"] = new Color(80, 0, 0),
        ["inputbackground"] = new Color(64, 0, 0),
        ["button"] = new Color(255, 0, 0),
        ["invalid"] = new Color(255, 255, 0),
        ["highlight"] = new Color(255, 255, 255),
        ["outercontrols"] = new Color(255, 255, 0),
        ["text"] = Color.White,
      }
    };
  }
}