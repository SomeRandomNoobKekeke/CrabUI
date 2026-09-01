using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework.Graphics;
namespace CursedUI
{
  public partial class CUIComponent
  {
    [CUISerializableProp]
    public bool Resizable
    {
      get => RightResizeHandle.Displayed;
      set
      {
        RightResizeHandle.Displayed = value;
        LeftResizeHandle.Displayed = value;
      }
    }

    [InitMethod]
    protected void InitHandles()
    {
      AttachResizeHandle(RightResizeHandle);
      AttachResizeHandle(LeftResizeHandle);
    }

    public ResizeHandle RightResizeHandle { get; } = new(new Vector2(1, 1))
    {
      Background ={
        Sprite = CUISprite.Angle,
        Effects = SpriteEffects.FlipHorizontally,
      }
    };

    public ResizeHandle LeftResizeHandle { get; } = new(new Vector2(0.0f, 1))
    {
      Background ={
        Sprite = CUISprite.Angle,
      }
    };
  }
}