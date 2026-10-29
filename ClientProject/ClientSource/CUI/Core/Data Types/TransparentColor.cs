using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;

namespace CursedUI
{

  public class TransparentColor
  {
    private Color _PureColor; public Color PureColor
    {
      get => _PureColor;
      set
      {
        _PureColor = value;
        CombineValues();
      }
    }

    private float _Transparency = 0.0f; public float Transparency
    {
      get => _Transparency;
      set
      {
        _Transparency = Math.Clamp(value, 0, 1);
        CombineValues();
      }
    }

    private void CombineValues()
    {
      Value = new Color(
        PureColor.R, PureColor.G, PureColor.B,
        (byte)Math.Round(PureColor.A * (1 - Transparency))
      );
    }

    public Color Value { get; private set; }

    public TransparentColor() { }
    public TransparentColor(Color pure) => PureColor = pure;
    public TransparentColor(Color pure, float transparency) => (PureColor, Transparency) = (pure, transparency);
  }
}