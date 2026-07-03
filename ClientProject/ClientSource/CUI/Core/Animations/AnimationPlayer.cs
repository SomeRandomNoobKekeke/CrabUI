using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using Barotrauma;

namespace CrabUI
{
  public class AnimationPlayer
  {
    public double UpdateStepDuration { get; set; } = Timing.Step;

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