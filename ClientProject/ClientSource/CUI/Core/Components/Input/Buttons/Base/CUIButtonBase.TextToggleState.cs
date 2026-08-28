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
    /// <summary>
    /// It's a wrapper for TextBlock that also marks layout when you set props
    /// </summary>
    public class TextToggleState_Part : Part
    {
      public TextBlock TextBlock { get; } = new();

      public Color BackgroundColor { get; set; }
      public Color BackgroundColorHovered { get; set; }



      public CUIRect Rect
      {
        get => TextBlock.Rect;
        set
        {
          TextBlock.Rect = value;
        }
      }

      public string Text
      {
        get => TextBlock.Text;
        set
        {
          TextBlock.Text = value;
          Self.LayoutMarker.Mark(LayoutMarker.Pattern.FromParentAndDown);
        }
      }

      public float Scale
      {
        get => TextBlock.Scale;
        set
        {
          TextBlock.Scale = value;
        }
      }

      public ResizeStrategy ResizeStrategy
      {
        get => TextBlock.ResizeStrategy;
        set
        {
          TextBlock.ResizeStrategy = value;
        }
      }

      public Vector2 TextAnchor
      {
        get => TextBlock.Anchor;
        set
        {
          TextBlock.Anchor = value;
        }
      }

      public Color TextColor
      {
        get => TextBlock.TextColor;
        set
        {
          TextBlock.TextColor = value;
          Self.LayoutMarker.Mark(LayoutMarker.Pattern.FromParentAndDown);
        }
      }

      public SpriteEffects SpriteEffects
      {
        get => TextBlock.SpriteEffects;
        set
        {
          TextBlock.SpriteEffects = value;
        }
      }

      public float LayerDepth
      {
        get => TextBlock.LayerDepth;
        set
        {
          TextBlock.LayerDepth = value;
        }
      }

      public CUIFont Font
      {
        get => TextBlock.Font;
        set
        {
          TextBlock.Font = value;
        }
      }

      public string RealText => TextBlock.RealText;
      public Vector2 RawTextSize => TextBlock.RawTextSize;
      public Vector2 TextDrawPosition => TextBlock.TextDrawPosition;
      public CUINullVector2 ForcedSize => TextBlock.ForcedSize;
      public float RealScale => TextBlock.RealScale;
    }
  }
}