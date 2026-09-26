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
using System.Xml.Linq;

namespace CursedUI
{
  public abstract partial class CUIButtonBase
  {
    /// <summary>
    /// It's a wrapper for TextBlock that also marks layout when you set props
    /// </summary>
    public class TextToggleState_Part : Part, CUISerializable
    {
      public TextBlock TextBlock { get; } = new();

      private Color _BackgroundColor; public Color BackgroundColor
      {
        get => _BackgroundColor;
        set
        {
          _BackgroundColor = value;
          Self.DetermineColor();
        }
      }
      private Color _BackgroundColorHovered; public Color BackgroundColorHovered
      {
        get => _BackgroundColorHovered;
        set
        {
          _BackgroundColorHovered = value;
          Self.DetermineColor();
        }
      }

      public CUIRect Rect
      {
        get => TextBlock.Rect;
        set
        {
          TextBlock.Rect = value;
        }
      }

      [CUISerializableProp]
      public string Text
      {
        get => TextBlock.Text;
        set
        {
          TextBlock.Text = value;
          Self.LayoutMarker.Mark(LayoutMarker.Pattern.AbsoluteProp);
        }
      }

      [CUISerializableProp]
      public float Scale
      {
        get => TextBlock.Scale;
        set
        {
          TextBlock.Scale = value;
        }
      }

      [CUISerializableProp]
      public ResizeStrategy ResizeStrategy
      {
        get => TextBlock.ResizeStrategy;
        set
        {
          TextBlock.ResizeStrategy = value;
        }
      }

      [CUISerializableProp]
      public Vector2 TextAnchor
      {
        get => TextBlock.Anchor;
        set
        {
          TextBlock.Anchor = value;
        }
      }

      [CUISerializableProp]
      public Color TextColor
      {
        get => TextBlock.TextColor;
        set
        {
          TextBlock.TextColor = value;
          Self.LayoutMarker.Mark(LayoutMarker.Pattern.FromParentAndDown);
        }
      }

      [CUISerializableProp]
      public SpriteEffects SpriteEffects
      {
        get => TextBlock.SpriteEffects;
        set
        {
          TextBlock.SpriteEffects = value;
        }
      }

      [CUISerializableProp]
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

      public static object Deserialize(XElement element)
      {
        throw new NotImplementedException();
      }
    }
  }
}