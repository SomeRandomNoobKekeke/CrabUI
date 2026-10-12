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
  public partial class SimpleTexture
  {
    protected ElevatedProp<Color> ElevatedColor { get; } = new();


    /// <summary>
    /// Source of truth elevated from sprite
    /// </summary>
    private bool _IgnoretransparentPixels;
    [CUISerializableProp]
    public bool IgnoretransparentPixels
    {
      get => _IgnoretransparentPixels;
      set
      {
        _IgnoretransparentPixels = value;
        _Sprite.ShouldBufferData = IgnoretransparentPixels;
      }
    }

    private CUISprite _Sprite = CUISprite.White;

    [CUISerializableProp]
    public CUISprite Sprite
    {
      get => _Sprite;
      set
      {
        _Sprite = value;
        _Sprite.ShouldBufferData = IgnoretransparentPixels;

        if (ElevatedColor.WasModified && !_Sprite.ElevatedColor.WasModified)
        {
          _Sprite.Color = ElevatedColor.Value;
        }
      }
    }
  }
}