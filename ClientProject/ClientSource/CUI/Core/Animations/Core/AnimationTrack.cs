using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Barotrauma;

namespace CrabUI
{
  public class AnimationTrack
  {
    //FIXME It's fragile, if animation is created before animation player it'll break
    public static double UpdateStepDuration => CUICore.AnimationPlayer.UpdateStepDuration;

    /// <summary>
    /// 0..1 - segment
    /// Speed - 1 step size
    /// 1.0 / Speed - amount of steps in animation
    /// Duration / UpdateStep - amount of steps in animation
    /// </summary>
    public double Duration
    {
      get => UpdateStepDuration / _Speed;
      set => _Speed = UpdateStepDuration / value;
    }

    private Func<double, double> _Func = f => f; public Func<double, double> Func
    {
      get => _Func;
      set => _Func = value is not null ? value : throw new ArgumentNullException(nameof(Func));
    }
    private double _Speed = 0.1f; public double Speed
    {
      get => _Speed;
      set => _Speed = Math.Max(0, value);
    }
    public ActionOnTrackEnd OnEnd { get; set; }
  }
}