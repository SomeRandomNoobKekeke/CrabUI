using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

namespace CrabUI
{
  // I'm not an expert in color theory, but at least some order
  public class CUIColorPreset
  {
    public Color Border;
    public Color Text;
    public Color Off;
    public Color OffHover;
    public Color On;
    public Color OnHover;
    public Color Disabled;
  }


  /// <summary>
  /// Contains some predefined values for components, mostly for colors  
  /// CUIPalette.Current is selected palette used by style resolver
  /// If you change it all components will recalculate their props
  /// There's just one palette for now :( , more to come
  /// </summary>
  public class CUIPalette
  {
    public static object Extract(string nestedName)
    {
      string[] names = nestedName.Split('.');
      if (names.Length == 0) return null;

      object result = null;

      FieldInfo fi = typeof(CUIPalette).GetField(names[0], AccessTools.all);
      PropertyInfo pi = typeof(CUIPalette).GetProperty(names[0], AccessTools.all);

      if (fi != null) result = fi.GetValue(null);
      if (pi != null) result = pi.GetValue(null);

      foreach (string name in names.Skip(1))
      {
        fi = result.GetType().GetField(name, AccessTools.all);
        pi = result.GetType().GetProperty(name, AccessTools.all);

        if (fi != null)
        {
          result = fi.GetValue(result);
          continue;
        }

        if (pi != null)
        {
          result = pi.GetValue(result);
          continue;
        }

        return null;
      }

      return result;
    }

    public CUIColorPreset Primary = new CUIColorPreset();
    public CUIColorPreset Secondary = new CUIColorPreset();
    public CUIColorPreset Tertiary = new CUIColorPreset();

    public static CUIPalette DarkBlue = new CUIPalette()
    {
      Primary = new CUIColorPreset()
      {
        Border = new Color(128, 128, 128, 255),
        Text = new Color(255, 255, 255, 255),
        Off = new Color(0, 0, 0, 128),
        Disabled = new Color(128, 128, 128, 255),
      },
      Secondary = new CUIColorPreset()
      {
        Border = new Color(100, 100, 100, 255),
        Text = new Color(255, 255, 255, 255),
        Off = new Color(0, 0, 32, 255),
        OffHover = new Color(0, 0, 64, 255),
        On = new Color(0, 0, 255, 255),
        OnHover = new Color(0, 0, 196, 255),
        Disabled = new Color(64, 64, 64, 255),
      },
      Tertiary = new CUIColorPreset()
      {
        Border = new Color(100, 100, 100, 255),
        Text = new Color(255, 255, 255, 255),
        Off = new Color(16, 0, 32, 255),
        OffHover = new Color(32, 0, 64, 255),
        On = new Color(255, 0, 255, 255),
        OnHover = new Color(196, 0, 196, 255),
        Disabled = new Color(32, 32, 32, 255),
      },
    };

    public static CUIPalette DarkRed = new CUIPalette()
    {
      Primary = new CUIColorPreset()
      {
        Border = new Color(128, 128, 128, 255),
        Text = new Color(255, 255, 255, 255),
        Off = new Color(32, 0, 0, 128),
        Disabled = new Color(128, 128, 128, 255),
      },
      Secondary = new CUIColorPreset()
      {
        Border = new Color(100, 100, 100, 255),
        Text = new Color(255, 255, 255, 255),
        Off = new Color(0, 0, 32, 255),
        OffHover = new Color(0, 0, 64, 255),
        On = new Color(0, 0, 255, 255),
        OnHover = new Color(0, 0, 196, 255),
        Disabled = new Color(64, 64, 64, 255),
      },
      Tertiary = new CUIColorPreset()
      {
        Border = new Color(100, 100, 100, 255),
        Text = new Color(255, 255, 255, 255),
        Off = new Color(16, 0, 32, 255),
        OffHover = new Color(32, 0, 64, 255),
        On = new Color(255, 0, 255, 255),
        OnHover = new Color(196, 0, 196, 255),
        Disabled = new Color(32, 32, 32, 255),
      },
    };

    public static CUIPalette Default => DarkBlue;
    private static CUIPalette current = Default;
    /// <summary>
    /// Palette used by style resolver
    /// </summary>
    public static CUIPalette Current
    {
      get => current;
      set
      {
        current = value ?? Default;
        CUIGlobalStyleResolver.OnPaletteChange(current);
      }
    }

    static CUIPalette()
    {
      //current = Default;
    }

    //TODO what if object has to be null? mb return CUIResult?
  }
}