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
  public class GlobalFocusTracker : IModule
  {
    public bool FocusShouldBeLost { get; set; }
    public CUIComponent ShouldBeFocused { get; set; }


    private CUIComponent _FocusedComponent;
    public CUIComponent FocusedComponent
    {
      get => _FocusedComponent;
      private set
      {
        if (_FocusedComponent is not null)
          _FocusedComponent = value;
      }
    }

    public void Reset()
    {
      FocusShouldBeLost = false;
      ShouldBeFocused = null;
    }

    public void ResolveFocus(bool SomethingFocusedElsewhere)
    {

    }
  }
}