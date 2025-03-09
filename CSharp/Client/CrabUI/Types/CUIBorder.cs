using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{

  //TODO why is this mutable?
  public class CUIBorder : ICloneable
  {
    private Color color; public Color Color
    {
      get => color;
      set
      {
        color = value;
        UpdateVisible();
      }
    }
    private float thickness = 1f; public float Thickness
    {
      get => thickness;
      set
      {
        thickness = value;
        UpdateVisible();
      }
    }

    public bool Visible { get; set; }


    public void UpdateVisible()
    {
      Visible = Thickness != 0f && color != Color.Transparent;
    }

    public CUIBorder() { }
    public CUIBorder(Color color, float thickness = 1f)
    {
      this.color = color;
      this.thickness = thickness;
      UpdateVisible();
    }

    public override bool Equals(object obj)
    {
      if (obj is CUIBorder border)
      {
        if (Color == border.Color && Thickness == border.Thickness) return true;
      }
      return false;
    }

    public object Clone()
    {
      return new CUIBorder(Color, Thickness);
    }


    public override string ToString() => $"[{CUIExtensions.ColorToString(Color)},{Thickness}]";
    public static CUIBorder Parse(string raw)
    {
      CUIBorder border = new CUIBorder();
      try
      {
        string[] sub = raw.Split('[', ']');

        if (sub.Length == 1)
        {
          border.Color = CUIExtensions.ParseColor(sub[0]);
        }

        if (sub.Length > 1)
        {
          string content = raw.Split('[', ']').ElementAtOrDefault(1);

          if (content.Trim() != "")
          {
            IEnumerable<string> values = content.Split(',');
            if (values.ElementAtOrDefault(0) != null)
            {
              border.Color = CUIExtensions.ParseColor(values.ElementAtOrDefault(0));
            }
            if (values.ElementAtOrDefault(1) != null)
            {
              float t = 1f;
              if (float.TryParse(values.ElementAtOrDefault(1), out t))
              {
                border.Thickness = t;
              }
            }
          }
        }

      }
      catch (Exception e) { CUI.Warning($"Couldn't parse CUIBorder [{raw}]:\n{e.Message}"); }

      return border;
    }
  }
}