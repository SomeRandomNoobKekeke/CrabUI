using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  //Note that later i might split it into multiple dicts, interface should stay the same
  public partial class CUIPalette : ReactiveDict<string, Color>
  {
    public Color BaseColor { get; set; }

    public static CUIPalette FromColor(Color color)
    {
      float brightness = color.Brightness();

      Color front = color;
      Color back = Color.Lerp(Color.Black, color, 0.1f);
      Color panel = Color.Lerp(Color.Black, color, 0.3f);
      Color border = Color.Lerp(Color.Black, color, 0.7f);
      Color text = Color.Lerp(Color.Black, Color.White, 0.95f);
      Color controls = new Color(0, 255, 255);
      Color selection = new Color(0, 255, 255);
      Color valid = new Color(0, 255, 0);
      Color invalid = new Color(255, 0, 0);
      Color disabled = new Color(64, 64, 64);

      return new CUIPalette()
      {
        ["main"] = front,
        ["back"] = back,
        ["panel"] = panel,
        ["border"] = border,
        ["text"] = text,
        ["controls"] = controls,
        ["selection"] = selection,
        ["valid"] = valid,
        ["invalid"] = invalid,
        ["disabled"] = disabled,

        BaseColor = color,
      };
    }

    public override string ToString() => $"Palette: {Logger.Wrap.Dictionary(this)}";
  }
}