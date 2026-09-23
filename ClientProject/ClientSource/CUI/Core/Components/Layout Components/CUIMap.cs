using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIMap : CUIComponent, IComponent
  {
    private float _Zoom; public float Zoom
    {
      get => _Zoom;
      set
      {
        _Zoom = value;

        VisualBounds.TransformMatrix =
          Matrix.CreateScale(Zoom) *
          Matrix.CreateTranslation(Rect.Center.X, Rect.Center.Y, 0f);
      }
    }

    protected override void UpdateRects()
    {
      base.UpdateRects();

      VisualBounds.TransformMatrix =
        Matrix.CreateScale(Zoom) *
        Matrix.CreateTranslation(Rect.Center.X, Rect.Center.Y, 0f);
    }
    public CUIMap()
    {
      ConsumeMouseEvents = true;



    }
  }
}