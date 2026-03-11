using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUIInput
  {
    private IInputProvider InputProvider;
    private InputSettings InputSettings;

    public MouseInput Mouse { get; }
    public bool SomethingHappened => Mouse.SomethingHappened;

    public void Update(double totalTime)
    {
      Mouse.Update(totalTime, InputProvider.ScanMouse());
    }

    public CUIInput(IInputProvider inputProvider, InputSettings inputSettings)
    {
      InputProvider = inputProvider;
      InputSettings = inputSettings;
      Mouse = new MouseInput(inputSettings);
    }
  }
}