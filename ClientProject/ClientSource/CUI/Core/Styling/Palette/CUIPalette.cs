using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  //Note that later i might split it into multiple dicts, interface should stay the same
  public partial class CUIPalette : ReactiveDict<string, Color>
  {
    public static CUIPalette FromColor(Color color)
    {
      Color front = color;
      Color back = new Color(0, 0, 0);
      Color text = new Color(255, 255, 255);
      Color controls = new Color(0, 255, 255);
      Color selection = new Color(0, 255, 255);
      Color valid = new Color(0, 255, 0);
      Color invalid = new Color(255, 0, 0);
      Color disabled = new Color(64, 64, 64);

      return new CUIPalette()
      {
        ["main"] = front,
        ["back"] = back,
        ["text"] = text,
        ["controls"] = controls,
        ["selection"] = selection,
        ["valid"] = valid,
        ["invalid"] = invalid,
        ["disabled"] = disabled,
      };
    }
  }
}