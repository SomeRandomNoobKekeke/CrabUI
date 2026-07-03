using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using Barotrauma;

namespace CrabUI
{
  public struct AnimationTrack
  {
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

    private Func<double, double> _Func; public Func<double, double> Func
    {
      get => _Func;
      init => _Func = value is not null ? value : throw new ArgumentNullException(nameof(Func));
    }
    private double _Speed; public double Speed
    {
      get => _Speed;
      init => _Speed = Math.Max(0, value);
    }
    public ActionOnTrackEnd OnEnd { get; init; }

    public AnimationTrack()
    {
      _Func = f => f;
      _Speed = 0.1f;
      OnEnd = ActionOnTrackEnd.Stop;
    }
  }
}