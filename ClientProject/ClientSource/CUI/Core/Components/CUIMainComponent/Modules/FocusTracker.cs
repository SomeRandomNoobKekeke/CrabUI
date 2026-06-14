using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  /// <summary>
  /// This thing contains logic for finding focused component in just one CUIMainComponent
  /// </summary>
  public class FocusTracker : IModule
  {
    public bool FocusShouldBeLost { get; set; }
    public IEventConsumer ShouldBeFocused { get; set; }

    public void Reset()
    {
      FocusShouldBeLost = false;
      ShouldBeFocused = null;
    }

    public void Resolve(CUIInput Input, EventTargets targets)
    {
      if (targets.TopTarget is null)
      {
        if (Input.Mouse.M1.Down)
        {
          FocusShouldBeLost = true;
        }
      }
      else
      {
        if (Input.Mouse.M1.Down)
        {
          ShouldBeFocused = targets.TopTarget;
        }
      }
    }
  }
}