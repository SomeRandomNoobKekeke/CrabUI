using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  [GeneratedComponent]
  public partial class SimpleTexture : IComponent
  {
    public class Animations_Part : IPart
    {
      public SimpleTexture Self { get; set; }

      public TypedAnimation<Color> Color => Self.AnimatedColor.Animation;
    }

    [InitMethod]
    protected void InitAnimatedProps()
    {
      AnimatedColor.Changed += (cl) =>
      {
        ElevatedColor.Value = cl;
        Sprite.Color = cl;
      };
    }

    public Animations_Part Animations { get; } = new();

    protected AnimatedValue<Color> AnimatedColor { get; } = new();
  }
}