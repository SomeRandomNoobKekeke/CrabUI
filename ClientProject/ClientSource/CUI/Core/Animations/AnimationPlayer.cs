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
    private HashSet<AnimationCore> RunningAnimations = new();
    public static int step = 0;
    public static int maxStep = 0;

    public static double lastTime;
    public void Update(double deltaTime)
    {
      foreach (AnimationCore animation in RunningAnimations)
      {
        animation.Update(deltaTime);
      }
    }

    public void AddToRunning(AnimationCore animation) => RunningAnimations.Add(animation);
    public void RemoveFromRunning(AnimationCore animation) => RunningAnimations.Remove(animation);
  }
}