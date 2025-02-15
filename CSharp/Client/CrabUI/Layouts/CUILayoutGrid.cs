using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// A Grid containing children in its cells  
  /// Dividing host into rows and columns basing on GridTemplate  
  /// And then placing children basin on their GridCell 
  /// </summary>
  public class CUILayoutGrid : CUILayout
  {
    public enum TrackSizeType
    {
      Unknown,
      Absolute,
      Relative,
      Fractional,
    }

    public class GridTrack
    {
      public TrackSizeType Type;
      public float? Absolute;
      public float? Relative;
      public float? Fractional;
      public float Start;
      public float End;
      public float Size;

      public float RealSize(float hostSize)
      {
        float size = 0;

        if (Absolute.HasValue) size = Absolute.Value;
        if (Relative.HasValue) size = Relative.Value * hostSize;

        return size;
      }

      public GridTrack(string value)
      {
        value = value.Trim();

        float f = 0;
        if (value.EndsWith("fr"))
        {
          if (float.TryParse(value.Substring(0, value.Length - 2), out f))
          {
            Fractional = f;
            Type = TrackSizeType.Fractional;
          }
        }

        if (value.EndsWith("%"))
        {
          if (float.TryParse(value.Substring(0, value.Length - 1), out f))
          {
            Relative = f / 100f;
            Type = TrackSizeType.Relative;
          }
        }

        if (float.TryParse(value, out f))
        {
          Absolute = f;
          Type = TrackSizeType.Absolute;
        }
      }

      public override string ToString() => $"[{Absolute},{Relative},{Fractional}]";

    }

    List<GridTrack> Rows = new();
    List<GridTrack> Columns = new();

    public void CalculateTracks()
    {
      Rows.Clear();
      Columns.Clear();

      if (Host.GridTemplateRows != null)
      {
        foreach (string s in Host.GridTemplateRows.Split(' '))
        {
          Rows.Add(new GridTrack(s));
        }
      }

      if (Host.GridTemplateColumns != null)
      {
        foreach (string s in Host.GridTemplateColumns.Split(' '))
        {
          Columns.Add(new GridTrack(s));
        }
      }

      if (Rows.Count == 0) Rows.Add(new GridTrack("100%"));
      if (Columns.Count == 0) Columns.Add(new GridTrack("100%"));

      float x = 0;
      foreach (GridTrack track in Columns)
      {
        track.Start = x;
        track.Size = track.RealSize(Host.Real.Width);
        x += track.Size;
        track.End = x;
      }

      float y = 0;
      foreach (GridTrack track in Rows)
      {
        track.Start = y;
        track.Size = track.RealSize(Host.Real.Height);
        y += track.Size;
        track.End = y;
      }

    }


    internal override void Update()
    {
      if (Changed && Host.Children.Count > 0)
      {
        Host.InvokeOnLayoutUpdated();

        CalculateTracks();

        foreach (CUIComponent c in Host.Children)
        {
          float x = 0;
          float y = 0;
          float w = 0;
          float h = 0;

          int startCellX = 0;
          int startCellY = 0;
          if (c.GridStartCell != null)
          {
            startCellX = Math.Clamp(c.GridStartCell.Value.X, 0, Rows.Count);
            startCellY = Math.Clamp(c.GridStartCell.Value.Y, 0, Columns.Count);
          }

          int endCellX = 0;
          int endCellY = 0;
          if (c.GridEndCell != null)
          {
            endCellX = Math.Clamp(c.GridEndCell.Value.X, 0, Rows.Count);
            endCellY = Math.Clamp(c.GridEndCell.Value.Y, 0, Columns.Count);
          }

          CUIRect real = new CUIRect(
            Columns[startCellX].Start,
            Rows[startCellY].Start,
            Columns[endCellX].End - Columns[startCellX].Start,
            Rows[endCellY].End - Rows[startCellY].Start
          );

          real = real.Shift(Host.Real.Position);

          c.AmIOkWithThisSize(real.Size);

          c.SetReal(real, "Grid Layout update");
        }
      }

      base.Update();
    }

    public CUILayoutGrid() : base() { }
    public CUILayoutGrid(CUIComponent host) : base(host) { }
  }
}