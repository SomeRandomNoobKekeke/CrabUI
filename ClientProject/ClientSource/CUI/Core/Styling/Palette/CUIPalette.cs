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

      return new CUIPalette()
      {
        ["button"] = front,
        ["text"] = text,
        ["frame"] = Color.Lerp(front, back, 0.9f),
        ["outercontrols"] = front,
        ["inputfocused"] = color.MultOpaque(0.8f),
        ["inputblured"] = color.MultOpaque(0.2f),
        ["inputselection"] = selection,
        ["inputcaret"] = selection,
        ["inputinvalid"] = invalid,
        ["border"] = front,
      };
    }

    public static CUIPalette Red => new()
    {
      ["button"] = new Color(255, 0, 0),
      ["text"] = new Color(255, 255, 255),
      ["frame"] = new Color(32, 0, 0),
      ["outercontrols"] = new Color(255, 200, 200),
      ["inputfocused"] = new Color(200, 0, 0),
      ["inputblured"] = new Color(64, 0, 0),
      ["inputselection"] = new Color(255, 255, 255),
      ["inputcaret"] = new Color(255, 255, 255),
      ["inputinvalid"] = new Color(255, 255, 0),
      ["border"] = new Color(100, 0, 0),
    };
  }
}