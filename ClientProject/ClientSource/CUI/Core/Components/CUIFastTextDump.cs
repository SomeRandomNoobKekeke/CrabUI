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

namespace CursedUI
{
  //LINK:\ClientProject\Tests\CUITest\Snapshots\Tests\Dynamic\CUIFastTextDump.cs
  public partial class CUIFastTextDump : CUIComponent, IComponent
  {
    public FastTextDump FastTextDump { get; } = new();


    public int Skip { get => FastTextDump.Skip; set => FastTextDump.Skip = value; } //Useless
    public IntRange? Range { get => FastTextDump.Range; set => FastTextDump.Range = value; } //Useless
    public List<FastTextDump.Line> Lines
    {
      get => FastTextDump.Lines;
      set
      {
        FastTextDump.Lines = value;
        if (FastTextDump.Solid) LayoutMarker.Mark(LayoutMarker.Pattern.AbsoluteProp);
      }
    }

    public void AddLine(string text, Color? color = null)
    {
      FastTextDump.AddLine(text, color);
      if (FastTextDump.Solid) LayoutMarker.Mark(LayoutMarker.Pattern.AbsoluteProp);
    }
    public void Clear()
    {
      FastTextDump.Clear();
      if (FastTextDump.Solid) LayoutMarker.Mark(LayoutMarker.Pattern.AbsoluteProp);
    }


    protected override void UpdateRects()
    {
      base.UpdateRects();
      FastTextDump.Rect = ChildrenRect;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        FastTextDump.Visible = value;
      }
    }

    public bool Solid { get => FastTextDump.Solid; set => FastTextDump.Solid = value; }
    public Vector2 Shift { get => FastTextDump.Shift; set => FastTextDump.Shift = value; }
    public Vector2 Offset { get => FastTextDump.Offset; set => FastTextDump.Offset = value; }
    public float Rotation { get => FastTextDump.Rotation; set => FastTextDump.Rotation = value; }
    public Vector2 Origin { get => FastTextDump.Origin; set => FastTextDump.Origin = value; }
    public float Scale { get => FastTextDump.Scale; set => FastTextDump.Scale = value; }
    public SpriteEffects SpriteEffects { get => FastTextDump.SpriteEffects; set => FastTextDump.SpriteEffects = value; }
    public float LayerDepth { get => FastTextDump.LayerDepth; set => FastTextDump.LayerDepth = value; }
    public Alignment TextAlignment { get => FastTextDump.TextAlignment; set => FastTextDump.TextAlignment = value; }
    public ForceUpperCase ForceUpperCase { get => FastTextDump.ForceUpperCase; set => FastTextDump.ForceUpperCase = value; }
    public CUIFont Font { get => FastTextDump.Font; set => FastTextDump.Font = value; }


    //TODO use padding
    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      FastTextDump.ForcedSize.X,
      FastTextDump.ForcedSize.Y
    );

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      if (CullChildren)
      {
        yield return VisualBounds.LeftBound;
        yield return FastTextDump.VisualWrapper;
        yield return VisualBounds.RightBound;
      }
      else
      {
        yield return FastTextDump.VisualWrapper;
      }

      yield return Border.VisualWrapper;
    }
  }
}