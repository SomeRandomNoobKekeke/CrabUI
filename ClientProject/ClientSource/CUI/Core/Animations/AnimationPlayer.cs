using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Barotrauma;

namespace CursedUI
{
  public class AnimationPlayer
  {
    public const double UpdateStepDuration = Timing.Step;
    public const double UpdateStepsInSecond = 1.0 / Timing.Step;

    private HashSet<AnimationCore> RunningAnimations = new();
    public void Update()
    {
      foreach (AnimationCore animation in RunningAnimations)
      {
        animation.Update();
      }
    }

    public void AddToRunning(AnimationCore animation) => RunningAnimations.Add(animation);
    public void RemoveFromRunning(AnimationCore animation) => RunningAnimations.Remove(animation);
  }
}