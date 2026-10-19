using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIPalettes
  {
    private CUIPalette _Primary = CUIPalette.FromColor(new Color(50, 50, 50, 255)); public CUIPalette Primary
    {
      get => _Primary;
      set => _Primary.Swap(value);
    }

    private CUIPalette _Secondary = CUIPalette.FromColor(new Color(100, 100, 100, 255)); public CUIPalette Secondary
    {
      get => _Secondary;
      set => _Secondary.Swap(value);
    }

    private CUIPalette _Tertiary = CUIPalette.FromColor(new Color(80, 80, 80, 255)); public CUIPalette Tertiary
    {
      get => _Tertiary;
      set => _Tertiary.Swap(value);
    }

    private CUIPalette _Quaternary = CUIPalette.FromColor(new Color(80, 0, 0, 255)); public CUIPalette Quaternary
    {
      get => _Quaternary;
      set => _Quaternary.Swap(value);
    }

    private CUIPalette _Quinary = CUIPalette.FromColor(new Color(0, 0, 0, 255)); public CUIPalette Quinary
    {
      get => _Quinary;
      set => _Quinary.Swap(value);
    }

    private CUIPalette _Senary = CUIPalette.FromColor(new Color(0, 0, 0, 255)); public CUIPalette Senary
    {
      get => _Senary;
      set => _Senary.Swap(value);
    }

    private CUIPalette _Septenary = CUIPalette.FromColor(new Color(0, 0, 0, 255)); public CUIPalette Septenary
    {
      get => _Septenary;
      set => _Septenary.Swap(value);
    }

    private CUIPalette _Octonary = CUIPalette.FromColor(new Color(0, 0, 0, 255)); public CUIPalette Octonary
    {
      get => _Octonary;
      set => _Octonary.Swap(value);
    }

    public CUIPalette FromName(string name)
    {
      PropertyInfo pi = typeof(CUIPalettes).GetProperty(name);
      if (pi is null) return Primary;
      if (pi.PropertyType != typeof(CUIPalette)) return Primary;

      return (CUIPalette)pi.GetValue(null)!;
    }
  }
}