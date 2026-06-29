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

  public class CUISegment
  {
    private float _Left; public float Left
    {
      get => _Left;
      set
      {
        _Left = value;
        _Width = Right - Left;
      }
    }

    private float _Right; public float Right
    {
      get => _Right;
      set
      {
        _Right = value;
        _Width = Right - Left;
      }
    }

    private float _Width; public float Width
    {
      get => _Width;
      set
      {
        _Width = value;
        _Right = Left + value;
      }
    }

    public override string ToString() => $"[{Left},{Right}]";
  }
}