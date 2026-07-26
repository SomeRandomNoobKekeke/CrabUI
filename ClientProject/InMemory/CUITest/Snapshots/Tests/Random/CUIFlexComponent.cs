using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      //Ass you can see doesn't really work
      public static CUIVisualComponent CUIFlexComponent()
      {
        CUIFlexComponent flexComponent = new();

        flexComponent.FlexTexture.Sprite = CUIFlexSprite.BaroDev;

        flexComponent.FlexTexture.FlexRect = new CUIFlexRect()
        {
          LT = new Vector2(500, 400),
          RT = new Vector2(1100, 400),
          RB = new Vector2(900, 700),
          LB = new Vector2(700, 700),
        };

        return flexComponent;
      }
    }
  }
}