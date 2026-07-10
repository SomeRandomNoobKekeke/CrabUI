using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{

  public class FocusTracker : IModule
  {
    public bool FocusShouldBeLost { get; set; }
    public IFocusable ShouldBeFocused { get; set; }

    public void Reset()
    {
      FocusShouldBeLost = false;
      ShouldBeFocused = null;
    }

    public void RequestFocus(IFocusable c) => ShouldBeFocused = c;

    public void CheckFocusLost(CUIInput input, EventTargets targets)
    {
      //TODO why only on click?
      //There was a click but no one wants to be focused
      if (ShouldBeFocused is null)
      {
        if (input.Mouse.M1.Down)
        {
          FocusShouldBeLost = true;
        }
      }
    }
  }
}