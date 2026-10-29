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
    private static Dictionary<Color, CUIPalette> FromColorPalettesCache = [];

    public static Color UltimateBackColor => Color.Black;

    public static CUIPalette Darken(CUIPalette palette, float amount)
    {
      if (palette.BaseColor.HasValue)
      {
        return CUIPalette.FromColor(palette.BaseColor.Value.To(UltimateBackColor, amount));
      }
      return palette;
    }

    public Color? BaseColor { get; set; }

    public void Swap(CUIPalette other)
    {
      if (other.BaseColor.HasValue) BaseColor = other.BaseColor;
      base.Swap(other);
    }

    public static CUIPalette FromColor(Color color)
    {
      if (FromColorPalettesCache.TryGetValue(color, out CUIPalette? palette))
      {
        return palette;
      }

      return FromColorPalettesCache[color] = CreateFromColor(color);
    }

    private static CUIPalette CreateFromColor(Color color)
    {
      Color black = new Color((byte)0, (byte)0, (byte)0, (byte)color.A);
      Color white = new Color((byte)255, (byte)255, (byte)255, (byte)color.A);

      float brightness = Math.Max(color.R, Math.Max(color.G, color.B)) / 255.0f;

      Color front = color;
      Color back = Color.Lerp(black, color, 0.2f);
      Color panel = Color.Lerp(back, color, 0.4f);
      Color backpanel = Color.Lerp(back, color, 0.1f);
      Color border = Color.Lerp(back, color, 0.6f);
      Color text = Color.Lerp(Color.Black, Color.White, 0.95f - (1.0f - brightness) * 0.2f);
      Color selection = new Color(0, 255, 255);
      Color valid = new Color(0, 255, 0);
      Color invalid = new Color(255, 0, 0);
      Color disabled = color.To(new Color(64, 64, 64), 0.75f);

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

    //TODO it feels weird that "value" type CUIPalette can be serialized to a reference to CUIPalettes
    // mb these methods should serialize as data, and CUICore.Parser should have extra parsing methods that would hide these and they would compare and restore by reference
    public static object Parse(string raw)
    {
      PropertyInfo pi = typeof(CUIPalettes).GetProperty(raw);

      if (pi?.PropertyType == typeof(CUIPalette))
      {
        return (CUIPalette)pi.GetValue(CUICore.Palettes)!;
      }

      return FromColor(CUICore.Parser.Parse<Color>(raw));
    }

    public override string ToString() => $"Palette: {ToText()}";

    public string ToText()
    {
      foreach (var (name, palette) in CUICore.Palettes.All)
      {
        if (palette == this) return name;
      }

      if (BaseColor.HasValue) return CUICore.Parser.Serialize(BaseColor.Value);
      return "Primary";
    }


  }
}