using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public partial class TypedAnimation<T>
  {
    public double Lambda => Core.Lambda;
    public AnimationDirection Direction
    {
      get => Core.Direction;
      set => Core.Direction = value;
    }
    public AnimationTrack Forward
    {
      get => Core.Forward;
      set => Core.Forward = value;
    }
    public AnimationTrack Backward
    {
      get => Core.Backward;
      set => Core.Backward = value;
    }

    public double Duration
    {
      get => Core.Duration;
      set => Core.Duration = value;
    }

    public Func<double, double> Func
    {
      get => Core.Func;
      set => Core.Func = value;
    }

    public double Speed
    {
      get => Core.Speed;
      set => Core.Speed = value;
    }

    public ActionOnTrackEnd OnEnd
    {
      get => Core.OnEnd;
      set => Core.OnEnd = value;
    }

    public bool IsRunning
    {
      get => Core.IsRunning;
      set => Core.IsRunning = value;
    }

    public void Start() => Core.Start();
    public void Stop() => Core.Start();
    public void Update() => Core.Start();

    public event Action Ended
    {
      add => Core.Ended += value;
      remove => Core.Ended -= value;
    }
    public event Action Started
    {
      add => Core.Ended += value;
      remove => Core.Ended -= value;
    }
    public event Action<double> Updated
    {
      add => Core.Updated += value;
      remove => Core.Updated -= value;
    }

  }
}