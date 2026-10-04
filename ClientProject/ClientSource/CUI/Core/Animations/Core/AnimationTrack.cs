using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Barotrauma;

namespace CursedUI
{
  public class AnimationTrack
  {

    public double Duration
    {
      get => 1.0 / _Speed;
      set => _Speed = 1.0 / value;
    }

    private Func<double, double> _Func = f => f; public Func<double, double> Func
    {
      get => _Func;
      set => _Func = value is not null ? value : throw new ArgumentNullException(nameof(Func));
    }

    private double _Speed = 1.0; public double Speed
    {
      get => _Speed;
      set => _Speed = Math.Max(0, value);
    }
    public ActionOnTrackEnd OnEnd { get; set; }
  }
}