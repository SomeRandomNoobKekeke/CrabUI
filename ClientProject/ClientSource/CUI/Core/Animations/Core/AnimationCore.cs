using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class AnimationCore
  {
    public static CUISegment PointerBounds { get; } = new CUISegment(0, 1);

    public double Pointer { get; private set; }
    public double Lambda => CurrentTrack.Func(Pointer);

    public AnimationDirection Direction { get; set; }
    public AnimationTrack Forward { get; set; } = new();
    public AnimationTrack Backward { get; set; } = new();

    public double Duration
    {
      get => Forward.Duration;
      set
      {
        CUI.Logger.Log($"-> {value}");
        Forward = Forward with { Duration = value };
        Backward = Backward with { Duration = value };
        CUI.Logger.LogVars(Forward.Duration);
      }
    }

    public Func<double, double> Func
    {
      get => Forward.Func;
      set
      {
        Forward = Forward with { Func = value };
        Backward = Backward with { Func = (f) => value(PointerBounds.Right - f) };
      }
    }
    public double Speed
    {
      get => Forward.Speed;
      set
      {
        Forward = Forward with { Speed = value };
        Backward = Backward with { Speed = value };
      }
    }
    public ActionOnTrackEnd OnEnd
    {
      get => Forward.OnEnd;
      set
      {
        Forward = Forward with { OnEnd = value };
        Backward = Backward with { OnEnd = value };
      }
    }

    public AnimationTrack CurrentTrack => Direction == AnimationDirection.Forward ? Forward : Backward;
    private bool PointerOutOfBounds(double p)
      => Direction == AnimationDirection.Forward ?
        p > PointerBounds.Right :
        p < PointerBounds.Left;

    private double SignedSpeed => Direction == AnimationDirection.Forward ? CurrentTrack.Speed : -CurrentTrack.Speed;
    private double StartPoint => Direction == AnimationDirection.Forward ? PointerBounds.Left : PointerBounds.Right;
    private double EndPoint => Direction == AnimationDirection.Forward ? PointerBounds.Right : PointerBounds.Left;
    private AnimationDirection OtherDirection => Direction == AnimationDirection.Forward ?
      AnimationDirection.Backward :
      AnimationDirection.Forward;

    private bool _IsRunning; public bool IsRunning
    {
      get => _IsRunning;
      set
      {
        if (value) Start(); else Stop();
      }
    }

    public Action OnEnded { set { Ended += value; } }
    public event Action Ended;

    public Action OnStarted { set { Started += value; } }
    public event Action Started;

    public Action<double> OnUpdated { set { Updated += value; } }
    public event Action<double> Updated;

    public void Start()
    {
      if (IsRunning) return;
      _IsRunning = true;

      CUICore.AnimationPlayer.AddToRunning(this);
      Started?.Invoke();
    }

    public void Stop()
    {
      if (!IsRunning) return;
      _IsRunning = false;

      CUICore.AnimationPlayer.RemoveFromRunning(this);
      Ended?.Invoke();
    }

    public void RunForward()
    {
      Direction = AnimationDirection.Forward;
      Start();
    }

    public void RunBackward()
    {
      Direction = AnimationDirection.Backward;
      Start();
    }


    public void Update()
    {
      Step();
      UpdateState();
      Updated?.Invoke(Lambda);
    }

    private void Step()
    {
      Pointer += SignedSpeed;
    }

    private void UpdateState()
    {
      if (PointerOutOfBounds(Pointer))
      {
        if (CurrentTrack.OnEnd == ActionOnTrackEnd.Stop)
        {
          Pointer = EndPoint;
          Stop();
        }

        if (CurrentTrack.OnEnd == ActionOnTrackEnd.Repeat)
        {
          Pointer = StartPoint;
        }

        if (CurrentTrack.OnEnd == ActionOnTrackEnd.Bounce)
        {
          Pointer = EndPoint;
          Direction = OtherDirection;
        }
      }
    }
  }
}