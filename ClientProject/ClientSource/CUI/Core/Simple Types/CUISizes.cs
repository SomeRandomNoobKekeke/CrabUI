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

  public struct CUISizes
  {
    public static CUIRect Zero { get; } = new CUIRect(0, 0, 0, 0);

    public float Left;
    public float Top;
    public float Right;
    public float Bottom;

    public float FullWidth => Left + Right;
    public float FullHeigth => Top + Bottom;

    public CUISizes(float left = 0, float top = 0, float right = 0, float bottom = 0)
    {
      Left = left;
      Top = top;
      Right = right;
      Bottom = bottom;
    }

    public override string ToString() => $"[{Left},{Top},{Right},{Bottom}]";
  }


}