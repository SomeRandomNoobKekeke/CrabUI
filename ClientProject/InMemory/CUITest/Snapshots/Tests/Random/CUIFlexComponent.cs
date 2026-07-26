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
      public static CUIVisualComponent CUIFlexComponent()
      {
        CUIFlexComponent flexComponent = new();

        flexComponent.FlexTexture.Sprite = CUIFlexSprite.BaroDev;

        flexComponent.FlexTexture.FlexRect = new CUIFlexRect()
        {
          LT = new Vector2(400, 400),
          RT = new Vector2(700, 300),
          RB = new Vector2(1000, 1000),
          LB = new Vector2(300, 700),
        };

        return flexComponent;
      }
    }
  }
}