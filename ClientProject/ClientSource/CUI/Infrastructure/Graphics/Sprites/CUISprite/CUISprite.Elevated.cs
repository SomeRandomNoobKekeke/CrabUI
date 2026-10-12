using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
namespace CursedUI
{
  public partial class CUISprite
  {
    //TODO how to make it non public?
    internal ElevatedProp<Color> ElevatedColor { get; } = new();
  }
}