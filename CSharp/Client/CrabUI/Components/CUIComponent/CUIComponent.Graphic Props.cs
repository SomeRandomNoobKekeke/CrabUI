using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent
  {
    /// <summary>
    /// Used for text, should be in CUITextBlock really
    /// </summary>
    [CUISerializable]
    public Vector2 Padding
    {
      get => CUIProps.Padding.Value;
      set => CUIProps.Padding.SetValue(value);
    }
    /// <summary>
    /// Should be one texture, not sprite sheet  
    /// Or there would be no way to wrap it  
    /// Top side will always point outwards
    /// </summary>
    [CUISerializable]
    public CUISprite BorderSprite { get; set; } = CUISprite.Default;

    /// <summary>
    /// Container for Color and Thickness  
    /// Border is drawn inside the component and will eat space from content  
    /// If "by side" border prop != null then it'll take presidence
    /// </summary>
    [CUISerializable] public CUIBorder Border { get; set; } = new CUIBorder();
    [CUISerializable] public CUIBorder TopBorder { get; set; }
    [CUISerializable] public CUIBorder RigthBorder { get; set; }
    [CUISerializable] public CUIBorder BottomBorder { get; set; }
    [CUISerializable] public CUIBorder LeftBorder { get; set; }


    [CUISerializable]
    public float OutlineThickness { get; set; } = 1f;
    /// <summary>
    /// Outline is like a border, but on the outside of the component
    /// </summary>
    [CUISerializable]
    public Color OutlineColor
    {
      get => CUIProps.OutlineColor.Value;
      set => CUIProps.OutlineColor.SetValue(value);
    }
    /// <summary>
    /// Will be drawn in background with BackgroundColor  
    /// Default is solid white 1x1 texture
    /// </summary>
    [CUISerializable]
    public CUISprite BackgroundSprite
    {
      get => CUIProps.BackgroundSprite.Value;
      set => CUIProps.BackgroundSprite.SetValue(value);
    }
    /// <summary>
    /// If true, mouse events on transparent pixels will be ignored  
    /// Note: this will buffer texture data and potentially consume a lot of memory
    /// so use wisely
    /// </summary>
    [CUISerializable]
    public bool IgnoreTransparent
    {
      get => CUIProps.IgnoreTransparent.Value;
      set => CUIProps.IgnoreTransparent.SetValue(value);
    }
    //TODO i think those colors could be stored inside sprites
    // But then it'll be much harder to apply side effects, think about it
    /// <summary>
    /// Color of BackgroundSprite, default is black  
    /// If you're using custom sprite and don't see it make sure this color is not black
    /// </summary>
    [CUISerializable]
    public Color BackgroundColor
    {
      get => CUIProps.BackgroundColor.Value;
      set => CUIProps.BackgroundColor.SetValue(value);
    }

    private float transparency = 1.0f;
    public float Transparency
    {
      get => transparency;
      set
      {
        transparency = value;
        foreach (CUIComponent child in Children)
        {
          if (!child.IgnoreParentTransparency) child.Transparency = value;
        }
      }
    }
    /// <summary>
    /// This palette will be used to resolve palette styles  
    /// Primary, Secondary, Tertiary, Quaternary
    /// </summary>
    [CUISerializable]
    public PaletteOrder Palette
    {
      get => CUIProps.Palette.Value;
      set => CUIProps.Palette.SetValue(value);
    }
    public PaletteOrder DeepPalette
    {
      set
      {
        Palette = value;
        foreach (var child in Children)
        {
          child.DeepPalette = value;
        }
      }
    }
    /// <summary>
    /// Had to expose resize handle props, because it's not a real component
    /// and can't really use styles
    /// </summary>
    [CUISerializable]
    public Color ResizeHandleColor { get; set; } = Color.White;
    [CUISerializable]
    public Color ResizeHandleGrabbedColor { get; set; } = Color.Cyan;

    /// <summary>
    /// don't
    /// </summary>
    public SamplerState SamplerState { get; set; }
  }
}