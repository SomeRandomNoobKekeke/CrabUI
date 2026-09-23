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
    public Camera Camera = new()
    {
      MinZoom = 0.25f,
      MaxZoom = 2f
    };

    private float _X = 0.1f; public float X
    {
      get => _X * 100;
      set
      {
        _X = value;
      }
    }

    private float _Y = 0.1f; public float Y
    {
      get => _Y;
      set
      {
        _Y = value + 0.1f;
      }
    }

    private float _Z = 0.1f; public float Z
    {
      get => _Z;
      set
      {
        CUI.Logger.Log(value);
        Camera.Zoom = value;
        Camera.UpdateTransform(interpolate: true, updateListener: false);

        VisualBounds.TransformMatrix = Camera.Transform;
      }
    }




    public CUIMap()
    {
      ConsumeMouseEvents = true;

      float height = 0f;


      this["x"] = new CUIRangeInput()
      {
        Absolute = new CUINullRect(0, 0, 300, 20),
        OnHandleDragged = (v) => X = v,
      };

      this["y"] = new CUIRangeInput()
      {
        Absolute = new CUINullRect(0, 30, 300, 20),
        OnHandleDragged = (v) => Y = v,
      };

      this["z"] = new CUIRangeInput()
      {
        Absolute = new CUINullRect(0, 60, 300, 20),
        OnHandleDragged = (v) => Z = v,
      };

    }
  }
}