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

        ["background"] = new Color(128, 0, 0),
        ["border"] = new Color(200, 0, 0),
        ["accent"] = new Color(255, 255, 0),
        ["text"] = Color.White,
      }
    };
  }
}