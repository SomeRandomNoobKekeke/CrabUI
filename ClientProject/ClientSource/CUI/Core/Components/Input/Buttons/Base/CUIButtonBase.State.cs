using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CursedUI
{
  public abstract partial class CUIButtonBase
  {
    public class State_Part : Part
    {
      public Color MouseOverColor { get; set; } = new Color(0, 0, 140);
      public Color MousePressedColor { get; set; } = new Color(0, 0, 200);
      public Color InactiveColor { get; set; } = new Color(0, 0, 100);
    }
  }
}