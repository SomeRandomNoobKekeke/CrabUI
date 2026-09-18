using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent AnimatedSprite()
      {
        CUIComponent frame = new CUIDefault.Frame("AnimatedSprite", 400, 600);

        return frame;
      }
    }
  }
}