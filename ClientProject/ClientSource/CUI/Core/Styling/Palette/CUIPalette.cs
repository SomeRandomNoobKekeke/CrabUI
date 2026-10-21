using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIPalette : WeakReactiveDict<string, Color>, IParsable
  {
    public Color? BaseColor { get; set; }

    public static CUIPalette FromColor(Color color)
    {
      Color black = new Color((byte)0, (byte)0, (byte)0, (byte)color.A);
      Color white = new Color((byte)255, (byte)255, (byte)255, (byte)color.A);

      float brightness = Math.Max(color.R, Math.Max(color.G, color.B)) / 255.0f;

      Color front = color;
      Color back = Color.Lerp(black, color, 0.2f);
      Color panel = Color.Lerp(back, color, 0.4f);
      Color backpanel = Color.Lerp(back, color, 0.2f);
      Color border = Color.Lerp(back, color, 0.6f);
      Color text = Color.Lerp(Color.Black, Color.White, 0.95f - (1.0f - brightness) * 0.2f);
      Color selection = new Color(0, 255, 255);
      Color valid = new Color(0, 255, 0);
      Color invalid = new Color(255, 0, 0);
      Color disabled = new Color(64, 64, 64);

      return new CUIPalette()
      {
        ["main"] = front,
        ["back"] = back,
        ["panel"] = panel,
        ["backpanel"] = backpanel,
        ["border"] = border,
        ["text"] = text,
        ["selection"] = selection,
        ["valid"] = valid,
        ["invalid"] = invalid,
        ["disabled"] = disabled,

        BaseColor = color,
      };
    }

    public static object Parse(string raw)
    {
      PropertyInfo pi = typeof(CUIPalettes).GetProperty(raw);

      if (pi?.PropertyType == typeof(CUIPalette))
      {
        return (CUIPalette)pi.GetValue(null)!;
      }

      return FromColor(CUICore.Parser.Parse<Color>(raw));
    }

    public override string ToString() => $"Palette: {ToText()}";

    public string ToText()
    {
      foreach (PropertyInfo pi in typeof(CUIPalettes).GetProperties(BindingFlags.Public | BindingFlags.Static))
      {
        if (pi.PropertyType != typeof(CUIPalette)) continue;
        if (pi.GetValue(null).Equals(this)) return pi.Name;
      }

      if (BaseColor.HasValue) return CUICore.Parser.Serialize(BaseColor.Value);
      return "Primary";
    }


  }
}