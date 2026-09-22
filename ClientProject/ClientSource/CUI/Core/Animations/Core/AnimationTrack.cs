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
    /// <summary>
    /// 0..1 - segment
    /// Speed - 1 step size
    /// 1.0 / Speed - amount of steps in animation
    /// Duration / AnimationPlayer.UpdateStepDuration - amount of steps in animation
    /// </summary>
    public double Duration
    {
      get => AnimationPlayer.UpdateStepDuration / _Speed;
      set => _Speed = AnimationPlayer.UpdateStepDuration / value;
    }

    private Func<double, double> _Func = f => f; public Func<double, double> Func
    {
      get => _Func;
      set => _Func = value is not null ? value : throw new ArgumentNullException(nameof(Func));
    }
    private double _Speed = 1.0 / AnimationPlayer.UpdateStepsInSecond; public double Speed
    {
      get => _Speed;
      set => _Speed = Math.Max(0, value);
    }
    public ActionOnTrackEnd OnEnd { get; set; }
  }
}