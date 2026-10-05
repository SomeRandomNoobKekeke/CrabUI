using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public partial class CUITextLine : CUIComponent, IComponent
  {
    public TextLine TextLine { get; } = new();

    [CUISerializableProp]
    public string Text
    {
      get => TextLine.Text;
      set => TextLine.Text = value;
    }

    [CUISerializableProp]
    public Color TextColor
    {
      get => TextLine.TextColor;
      set => TextLine.TextColor = value;
    }

    [CUISerializableProp]
    public float Rotation
    {
      get => TextLine.Rotation;
      set => TextLine.Rotation = value;
    }

    [CUISerializableProp]
    public Vector2 Origin
    {
      get => TextLine.Origin;
      set => TextLine.Origin = value;
    }

    [CUISerializableProp]
    public float Scale
    {
      get => TextLine.Scale;
      set => TextLine.Scale = value;
    }

    [CUISerializableProp]
    public SpriteEffects SpriteEffects
    {
      get => TextLine.SpriteEffects;
      set => TextLine.SpriteEffects = value;
    }

    [CUISerializableProp]
    public float LayerDepth
    {
      get => TextLine.LayerDepth;
      set => TextLine.LayerDepth = value;
    }

    [CUISerializableProp]
    public Alignment Alignment
    {
      get => TextLine.Alignment;
      set => TextLine.Alignment = value;
    }

    [CUISerializableProp]
    public ForceUpperCase ForceUpperCase
    {
      get => TextLine.ForceUpperCase;
      set => TextLine.ForceUpperCase = value;
    }

    // [CUISerializableProp]
    public CUIFont Font
    {
      get => TextLine.Font;
      set => TextLine.Font = value;
    }

    protected override void UpdateRects()
    {
      base.UpdateRects();
      TextLine.Position = Rect.LeftTop;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        TextLine.Visible = value;
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return TextLine.VisualWrapper;
    }

  }
}