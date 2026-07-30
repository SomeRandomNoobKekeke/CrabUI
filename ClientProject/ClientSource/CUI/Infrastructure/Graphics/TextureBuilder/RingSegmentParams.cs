using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{

  public class RingSegmentParams
  {
    public double _Angle; public double Angle
    {
      get => _Angle;
      set
      {
        _Angle = value;
        ComputeAngles();
      }
    }

    public double _AngleOffset; public double AngleOffset
    {
      get => _AngleOffset;
      set
      {
        _AngleOffset = value;
        ComputeAngles();
      }
    }

    private void ComputeAngles()
    {
      StartAngle = AngleOffset;
      EndAngle = AngleOffset + Angle;
    }

    public double StartAngle { get; set; }
    public double EndAngle { get; set; }

    public double MidAngle => (StartAngle + EndAngle) / 2.0f;


    public float _Height; public float Height
    {
      get => _Height;
      set
      {
        _Height = value;
        ComputeRadiuses();
      }
    }

    public float _Hole; public float Hole
    {
      get => _Hole;
      set
      {
        _Hole = value;
        ComputeRadiuses();
      }
    }

    public float _BorderFade = 2.0f; public float BorderFade
    {
      get => _BorderFade;
      set
      {
        _BorderFade = value;
        ComputeRadiuses();
      }
    }

    private void ComputeRadiuses()
    {
      StartRadius = Hole;
      EndRadius = StartRadius + Height;
      InnerFadeRadius = StartRadius - BorderFade;
      OuterFadeRadius = EndRadius + BorderFade;
    }
    public float StartRadius { get; set; }
    public float EndRadius { get; set; }
    public float InnerFadeRadius { get; set; }
    public float OuterFadeRadius { get; set; }


    public Color FillColor { get; set; } = new Color(255, 255, 255, 127);
    public Color BorderColor { get; set; } = new Color(255, 255, 255, 255);

    public Vector2 Origin { get; set; }
    public float Offset { get; set; }
  }

}